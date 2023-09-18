using GoogleMaps.LocationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.AssetOperation;
using TemplateTrack.Core.Interface.TrackAssetInfo;
using TemplateTrack.DataAccess.Model.NewFolder;
using TemplateTrack.DataAccess.Model.TraceInfo;
using TemplateTrack.DataAccess.Model.TrackingInfo;

namespace TemplateTrack.API.Controllers.TrackInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackAssetInfoController : ControllerBase
    {
        private readonly ITrackAssetInfo _trackAssetInfo;
        private readonly ApplicationDbContext _context;

        public TrackAssetInfoController(ITrackAssetInfo trackAssetInfo, ApplicationDbContext context)
        {
            _trackAssetInfo = trackAssetInfo;
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<AssetTrackingInfo>>> GetAllTrackInfo()
        {
            var result = await _trackAssetInfo.GetAllTrackInfo();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrackInfo(int id)
        {
            var result = await _trackAssetInfo.DeleteTrackInfo(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> addAssetLocation([FromBody] AssetTrackingInfo assetTrackingInfo)
        {
            var result = await _trackAssetInfo.addAssetLocation(assetTrackingInfo);
            return Ok(result);
        }

        /// <summary>
        /// Startrd Asset Tracking API
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet]
        [Route("LongLati_Manually")]
        public async Task<ActionResult<AssetTrackingInfo>> FetchLocation(int id)
        {
            var result = await _trackAssetInfo.TrackSpecificAsset(id);
            return Ok(result);
        }



        [HttpGet]
        [Route("Get_Address")]
        public async Task<IActionResult> GetLocation(double latitude, double longitude)
        {
            
                try
                {
                    string location = await _trackAssetInfo.GetLocation(latitude, longitude);

                    return Ok(location);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }



        [HttpGet]
        [Route("Find_Latest")]
        public async Task<IActionResult> LatestLocation(string barcode)
        {

            var location = await _trackAssetInfo.LatestLocation(barcode);

            if (location == null)
            {
                return NotFound("Address Generated Already");
            }
            return Ok(location);

        }



        [HttpGet]
        [Route("Current_Location")]
        public async Task<ActionResult<AssetTraceIfo>> GetAssetLocation(string barcode)
        {
            var ifExist = _context.assetTraceIfos.Where(x => x.BarCode == barcode);
            if (ifExist.Any())
            {
                var result = await _trackAssetInfo.GetAssetLocation(barcode);
                return Ok(result);
            }
            else
            {
                return NotFound("Record Not Found");
            }    
        }


    }
}
