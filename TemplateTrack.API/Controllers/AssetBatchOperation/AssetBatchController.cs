using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.AssetOperation;
using TemplateTrack.Core.Interface.BatchAssetOp;
using TemplateTrack.DataAccess.Model.BatchAsset;
using TemplateTrack.DataAccess.Model.NewFolder;

namespace TemplateTrack.API.Controllers.AssetBatchOperation
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssetBatchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBatchAsset _batchAsset;

        public AssetBatchController(ApplicationDbContext context, IBatchAsset batchAsset)
        {
            _context = context;
            _batchAsset = batchAsset;
        }

        [HttpGet]
        [Route("/batchRecord")]
        public async Task<ActionResult<List<AssetBatch>>> getAllAsset()
        {
            var result = await _batchAsset.GetAllBatchAsset();
            return Ok(result);
        }

        [HttpPost]
        [Route("AddassetBatch")]
        public async Task<IActionResult> addAsseBatch([FromBody] List<AssetBatch> assetBatchs)
        {
            var result = await _batchAsset.addAsseBatch(assetBatchs);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateBatchInfo")]
        public async Task<IActionResult> UpdateAsseBatch([FromBody] List<AssetBatch> assetBatchs)
        {
            var result = await _batchAsset.UpAsseBatch(assetBatchs);
            return Ok(result);
        }

    }
}
