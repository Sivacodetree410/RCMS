using System.Security.Cryptography;
using System.Text;

namespace RCMS.Services
{
    public class HMACDecryptService
    {
        public bool Verify(string iv,string cipher,string receivedHmac,byte[] key)
        {
            using var hmac = new HMACSHA256(key);

            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(iv + cipher));

            string calculated = Convert.ToBase64String(hash);

            return CryptographicOperations.FixedTimeEquals(Convert.FromBase64String(calculated),Convert.FromBase64String(receivedHmac));
        }
    }
}
