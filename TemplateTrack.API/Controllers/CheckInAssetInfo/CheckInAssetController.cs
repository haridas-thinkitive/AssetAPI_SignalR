using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.CheckInAsset;
using TemplateTrack.Core.Interface.IAssetAll;
using TemplateTrack.DataAccess.Model.AssetCheckInInfo;
using TemplateTrack.DataAccess.Model.AssetManagement;
using TemplateTrack.DataAccess.Model.NewFolder;

namespace TemplateTrack.API.Controllers.CheckInAssetInfo
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInAssetController : ControllerBase
    {
        private readonly IcheckInAsset _icheckInAsset;

        public CheckInAssetController(IcheckInAsset icheckInAsset)
        {
            _icheckInAsset = icheckInAsset;

        }

        [HttpGet]
        [Route("CheckInAsset")]
        public async Task<ActionResult<List<AssetCheckInInfo>>> getAllCheckInAsset()
        {
            var result = await _icheckInAsset.getAllCheckInAsset();
            return Ok(result);
        }

        [HttpPost]
        [Route("api/checkin/{barCodeNo}")]
        public async Task<ActionResult<AssetInfo>> CheckInAsset(string barCodeNo)
        {
            var asset = await _icheckInAsset.CheckInAsset(barCodeNo);

            if (asset == null)
            {
                return NotFound("Asset not found.");
            }
            return asset;
        }


        [HttpPost]
        [Route("api/checkOut/{barCodeNo}")]
        public async Task<ActionResult<AssetInfo>> CheckOutAsset(string barCodeNo)
        {
            var asset = await _icheckInAsset.CheckOutAsset(barCodeNo);

            if (asset == null)
            {
                return NotFound("Asset not found.");
            }
            return asset;
        }

    }
}
