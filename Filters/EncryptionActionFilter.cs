using Microsoft.AspNetCore.Mvc.Filters;
using RCMS.EncryptDecryptHelpers;
using RCMS.ModelHelpers;
using RCMS.Services;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace RCMS.Filters
{
    public class EncryptionActionFilter : IAsyncActionFilter
    {
        private readonly RSADecryptService _rsadec;
        private readonly AESCryptoService _aescrypto;
        private readonly HMACDecryptService _hmacdec;

        public EncryptionActionFilter(RSADecryptService rsadec, AESCryptoService aescrypto, HMACDecryptService hmacdec)
        {
            _rsadec=rsadec;
            _aescrypto=aescrypto;
            _hmacdec=hmacdec;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Request.EnableBuffering();

            using StreamReader reader = new StreamReader(context.HttpContext.Request.Body,Encoding.UTF8,leaveOpen: true);
            string body = await reader.ReadToEndAsync();

            context.HttpContext.Request.Body.Position = 0;
            SecureRequest request = JsonSerializer.Deserialize<SecureRequest>(body);

            byte[] aesKey =
                _rsadec.DecryptKey(request.EncryptedKey);

            bool valid =
                _hmacdec.Verify(
                    request.IV,
                    request.CipherText,
                    request.Hmac,
                    aesKey);

            if (!valid)
            {
                throw new Exception("Invalid HMAC");
            }

            string json =
                _aescrypto.AESCryptoDecrypt(
                    request.CipherText,
                    aesKey,
                    request.IV);

            byte[] bytes =
                Encoding.UTF8.GetBytes(json);

            context.HttpContext.Request.Body =
                new MemoryStream(bytes);

            context.HttpContext.Request.ContentLength =
                bytes.Length;

            await next();
           
        }
    }
}
