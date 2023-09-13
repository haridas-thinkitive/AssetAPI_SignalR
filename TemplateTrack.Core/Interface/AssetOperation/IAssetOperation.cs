using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.DataAccess.Model.NewFolder;

namespace TemplateTrack.Core.Interface.AssetOperation
{
    public interface IAssetOperation
    {
        Task<List<AssetInfo>> GetAllassetInfo();

        Task<int> addAsseInfo(AssetInfo assetInfos);

        Task<int> EditAsset(AssetInfo assetInfos, int id);
        Task<int>  DeleteAsset(int id);

        Task<AssetInfo> GetById(int id);




    }
}
