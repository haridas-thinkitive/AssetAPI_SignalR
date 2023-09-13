using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.AssetOperation;
using TemplateTrack.DataAccess.Model.NewFolder;

namespace TemplateTrack.API.Controllers.AssetDetailsOperation
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssetOperationController : ControllerBase
    {
        private readonly IAssetOperation _assetOperation;

        public AssetOperationController(IAssetOperation assetOperation)
        {
            _assetOperation = assetOperation;
        }

        [HttpGet]
        [Route("/GetAllAssetInfo")]
        public async Task<ActionResult<List<AssetInfo>>> getAllAsset()
        {
            var result = await _assetOperation.GetAllassetInfo() ;
            return Ok(result);
        }

        [HttpGet]
        [Route("/GetByID/{id}")]
        public async Task<ActionResult<AssetInfo>> GetById(int id)
        {
            var result = await _assetOperation.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("/AddAssetInfo"),]
        public async Task<IActionResult> addAsseInfo([FromBody] AssetInfo assetInfos)
        {
            var result = await _assetOperation.addAsseInfo(assetInfos) ;
            return Ok(result);
        }

        [HttpPut]
        [Route("/EditeAssetInfo/{id}")]
        public async Task<ActionResult<AssetInfo>> EditAsset(AssetInfo assetInfo, int id)
        {
            var result = await _assetOperation.EditAsset(assetInfo, id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("/DeleteAssetInfo/{id}")]
        public async  Task<ActionResult> DeleteAsset(int id)
        {
            var result = await _assetOperation.DeleteAsset(id);
            return Ok(result);
            
        }
        
    }
}
