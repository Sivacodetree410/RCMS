using RCMS.IRepositories;
using RCMS.ModelHelpers;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RCMS.EncryptDecryptHelpers
{
    public class AESHelper : IAESRepository
    {
        private readonly byte[] Key;

        public AESHelper(string key)
        {
            Key = Encoding.UTF8.GetBytes(key); // 32 bytes for AES-256  32 characters (256-bit)
        }

        public async Task<EncDecCModel> Encrypt(string plainText)
        {
            EncDecCModel encDecCModel = new EncDecCModel();
            try
            {
                using Aes aes = Aes.Create();

                aes.Key = Key;
                aes.GenerateIV(); // Random IV   16 characters (128-bit)  not more than 16 characters for IV 
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using MemoryStream ms = new MemoryStream();

                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    await sw.WriteAsync(plainText);
                }

                string iv = Convert.ToBase64String(aes.IV);
                string cipher = Convert.ToBase64String(ms.ToArray());

                string encrptstr = $"{iv}:{cipher}";  // IV:CipherText

                encDecCModel.Status = true;
                encDecCModel.EncDecData = encrptstr;
                encDecCModel.Msg = "Data Encrypted Successfully";

                //byte[] cipherBytes = ms.ToArray();

                //// Combine IV + CipherText
                //byte[] result = new byte[aes.IV.Length + cipherBytes.Length];

                //Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
                //Buffer.BlockCopy(cipherBytes, 0, result, aes.IV.Length, cipherBytes.Length);


                //encDecCModel.Status = true;
                //encDecCModel.EncDecData = Convert.ToBase64String(result);
                //encDecCModel.Msg = "Data Encrypted Successfully";

            }
            catch (Exception ex)
            {
                encDecCModel.Status = false;
                encDecCModel.ErrorMsg = ex.Message;
            }
            return encDecCModel;
        }

        public async Task<EncDecCModel> Decrypt(string encryptedData)
        {
            EncDecCModel encDecCModel = new EncDecCModel();
            try
            {
                string[] parts = encryptedData.Split(':');

                byte[] iv = Convert.FromBase64String(parts[0]);
                byte[] cipherBytes = Convert.FromBase64String(parts[1]);

                using Aes aes = Aes.Create();

                aes.Key = Key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using MemoryStream ms = new MemoryStream(cipherBytes);
                using CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
                using StreamReader sr = new StreamReader(cs);

                string decryptstr = await sr.ReadToEndAsync();

                encDecCModel.Status = true;
                encDecCModel.EncDecData = decryptstr;
                encDecCModel.Msg = "Data Decrypted Successfully";
            }
            catch (Exception ex)
            {
                encDecCModel.Status = false;
                encDecCModel.ErrorMsg = ex.Message;
            }
            return encDecCModel;
        }

    }
}
