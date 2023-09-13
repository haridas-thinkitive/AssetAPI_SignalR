using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemplateTrack.Core.Interface.AssetAlluser;
using TemplateTrack.DataAccess.Model.Registration;

namespace TemplateTrack.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AllAssetUserController : ControllerBase
    {
        private readonly IAllAssetUserService _assetAllUser;

        public AllAssetUserController(IAllAssetUserService assetAllUser)
        {
            _assetAllUser = assetAllUser;
        }

        [HttpGet]
        public async Task<ActionResult<List<RegistrationModel>>> GetAssetUser()
        {
            var result = await _assetAllUser.GetAllUser();
            return result;
        }
    }
}
