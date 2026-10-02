using System.Security.Cryptography;
using System.Text;

namespace RCMS.Services
{
    public class AESCryptoService
    {

        public string AESCryptoDecrypt(string cipherText,byte[] key,string iv)
        {
            byte[] cipher = Convert.FromBase64String(cipherText);

            byte[] ivBytes = Convert.FromBase64String(iv);

            using Aes aes = Aes.Create();

            aes.Key = key;
            aes.IV = ivBytes;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using ICryptoTransform decryptor = aes.CreateDecryptor();

            byte[] plain = decryptor.TransformFinalBlock(cipher,0,cipher.Length);

            return Encoding.UTF8.GetString(plain);
        }

    }
}
