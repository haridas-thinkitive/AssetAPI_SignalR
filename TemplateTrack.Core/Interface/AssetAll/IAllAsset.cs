using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.DataAccess.Model.AssetManagement;
using TemplateTrack.DataAccess.Model.Registration;

namespace TemplateTrack.Core.Interface.IAssetAll
{
    public interface IAllAsset
    {
        Task<List<Asset>> GetAllAssets();

        Task<Asset> GetAssetById(int assetId);

        Task<Asset> CheckInAsset(string barCodeNo);

    }
}
