using RCMS.DBHelpers;
using RCMS.IRepositories;
using RCMS.IServices;
using RCMS.ModelHelpers;
using System.Security.Cryptography;

namespace RCMS.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationsRepositories _registrationsRepositories; 
        public RegistrationService(IRegistrationsRepositories registrationsRepositories)
        {
            _registrationsRepositories = registrationsRepositories;
        }

        public async Task<CommonModel> RCMSRegistrations(RegistrationModel registration)
        {
            string salt = GenerateSalt();

            string pwdhash = HashPassword(registration.Input3, salt);

            registration.Input5 = pwdhash;
            registration.Input6 = salt;

            return await _registrationsRepositories.RCMSRegistrations(registration);
        }

        private string GenerateSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(32); // 256-bit salt
            return Convert.ToBase64String(salt);
        }

        private string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                saltBytes,
                100000,
                HashAlgorithmName.SHA256,
                32);

            return Convert.ToBase64String(hash);
        }
    }
}
