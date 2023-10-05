using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.IAssetAll;
using TemplateTrack.DataAccess.Model.AssetManagement;
using TemplateTrack.DataAccess.Model.Registration;

namespace TemplateTrack.API.Controllers.AllAsset
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AssetAllController : ControllerBase
    {

        private readonly IAllAsset _allAsset;
        private readonly ApplicationDbContext _context;

        public AssetAllController(IAllAsset allAsset, ApplicationDbContext context)
        {
            _allAsset = allAsset;
            _context = context;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("api/assets")]
        public async Task<ActionResult<List<Asset>>> GetAllAssets()
        {
            var result = await _allAsset.GetAllAssets();
            return result;
        }

        [HttpGet]
        [Route("api/assets/{assetId}")]
        public async Task<ActionResult<Asset>> GetAssetById(int assetId)
        {
            var result = await _allAsset.GetAssetById(assetId); // Await the asynchronous method.

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/assets/checkin/{barCodeNo}")]
        public async Task<ActionResult<Asset>> GetAssetById(string barCodeNo)
        {
            var asset = await _allAsset.CheckInAsset(barCodeNo);

            if(asset == null)
            {
                return NotFound("Asset not found.");
            }
            return asset;
        }
    }
}
