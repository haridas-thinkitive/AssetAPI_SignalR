using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using TemplateTrack.Core.Interface.BatchAssetOp;
using TemplateTrack.Core.Interface.Register;
using TemplateTrack.DataAccess.Model.Authentication.Login;
using TemplateTrack.DataAccess.Model.Authentication.SignUp;

namespace TemplateTrack.API.Controllers.Registration
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationRegisterController : ControllerBase
    {
        private readonly IRegister _registeredServices;

        public AuthenticationRegisterController(IRegister registeredServices)
        {
            _registeredServices = registeredServices;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterUser registerUser,string role)
        {
            var result = await _registeredServices.RegisterNewUser(registerUser,role);
            return Ok(result);
        }

        [HttpPost]
        [Route("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginModel loginModel)
        {
            var result = await _registeredServices.LoginUser(loginModel);
            return Ok(result);
        }
    }
}
