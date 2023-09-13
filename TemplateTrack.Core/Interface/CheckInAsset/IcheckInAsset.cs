using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.DataAccess.Model.AssetCheckInInfo;
using TemplateTrack.DataAccess.Model.AssetHistoryInfo;
using TemplateTrack.DataAccess.Model.AssetManagement;
using TemplateTrack.DataAccess.Model.BatchAsset;
using TemplateTrack.DataAccess.Model.NewFolder;

namespace TemplateTrack.Core.Interface.CheckInAsset
{
    public interface IcheckInAsset
    {
        Task<List<AssetCheckInInfo>> getAllCheckInAsset();

        Task<AssetInfo> CheckInAsset(string barCodeNo);

        Task<AssetInfo> CheckOutAsset(string barCodeNo);

    }
}
