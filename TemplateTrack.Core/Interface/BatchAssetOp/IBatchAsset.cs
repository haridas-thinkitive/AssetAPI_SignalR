using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.DataAccess.Model.BatchAsset;
using TemplateTrack.DataAccess.Model.NewFolder;

namespace TemplateTrack.Core.Interface.BatchAssetOp
{
    public interface IBatchAsset
    {
        Task<List<AssetBatch>> GetAllBatchAsset();

        Task<int> addAsseBatch(List<AssetBatch> assetInfos);

        Task<int> UpAsseBatch(List<AssetBatch> assetInfos);
        Task<string> BatchAsset(List<AssetBatch> assetInfos);
    }
}
