using RCMS.ModelHelpers;

namespace RCMS.IServices
{
    public interface IRegistrationService
    {
        Task<CommonModel> RCMSRegistrations(RegistrationModel registration);

    }
}
