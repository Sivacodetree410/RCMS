using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RCMS.IServices;
using RCMS.ModelHelpers;

namespace RCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RCMSRegistrationsController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RCMSRegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }


        [HttpPost]
        [Route("USERRegistrations")]
        public async Task<IActionResult> RCMSUSERRegistrations(RegistrationModel registration)
        {
            var result = await _registrationService.RCMSRegistrations(registration);
            return Ok(result);
        }
    }
}
