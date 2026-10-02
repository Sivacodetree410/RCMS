using RCMS.ModelHelpers;

namespace RCMS.IRepositories
{
    public interface IRegistrationsRepositories
    {
         Task<CommonModel> RCMSRegistrations(RegistrationModel registration);
    }
}
