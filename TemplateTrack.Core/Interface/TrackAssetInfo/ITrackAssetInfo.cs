using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.DataAccess.Model.AssetCheckInInfo;
using TemplateTrack.DataAccess.Model.NewFolder;
using TemplateTrack.DataAccess.Model.TraceInfo;
using TemplateTrack.DataAccess.Model.TrackingInfo;

namespace TemplateTrack.Core.Interface.TrackAssetInfo
{
    public interface ITrackAssetInfo
    {
        Task<List<AssetTrackingInfo>> GetAllTrackInfo();

        Task<AssetTrackingInfo> TrackSpecificAsset(int id);

        Task<string> GetLocation(double latitude, double longitude);

        Task<string> LatestLocation(string barcode);

        Task<AssetTraceIfo> GetAssetLocation(string barcode);
        Task<int> DeleteTrackInfo(int id);

        Task<int> addAssetLocation(AssetTrackingInfo assetTrackingInfo);

    }
}
