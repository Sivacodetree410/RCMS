using System.Security.Cryptography;

namespace RCMS.Services
{
    public class RSADecryptService
    {
        private readonly IConfiguration _configuration;

        public RSADecryptService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public byte[] DecryptKey(string encryptedKey)
        {
            using RSA rsa = RSA.Create();

            rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(_configuration["RSA:PrivateKey"]), out _);

            return rsa.Decrypt(Convert.FromBase64String(encryptedKey), RSAEncryptionPadding.OaepSHA256);
        }
    }
}
