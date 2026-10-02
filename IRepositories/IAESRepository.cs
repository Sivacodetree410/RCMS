using RCMS.ModelHelpers;

namespace RCMS.IRepositories
{
    public interface IAESRepository
    {
        Task<EncDecCModel> Encrypt(string plainText);

        Task<EncDecCModel> Decrypt(string encryptedData);
    }
}
