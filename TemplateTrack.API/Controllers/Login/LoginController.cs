using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemplateTrack.Core.Interface.LoginAsset;
using TemplateTrack.DataAccess.Model.Registration;

namespace TemplateTrack.API.Controllers.Login
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IAssetLogin _assetLogin;

        public LoginController(IAssetLogin assetLogin)
        {
            _assetLogin = assetLogin;
        }

        [HttpPost]
        public async Task<ActionResult<List<RegistrationModel>>> LoginAsset(string userName, string password)
        {
            var result = _assetLogin.LoginAsset(userName, password);
            return Ok(result);
        }

    }
}
