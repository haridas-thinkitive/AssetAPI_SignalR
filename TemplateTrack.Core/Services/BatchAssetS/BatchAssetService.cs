using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.BatchAssetOp;
using TemplateTrack.DataAccess.Model.BatchAsset;
using TemplateTrack.DataAccess.Model.NewFolder;

namespace TemplateTrack.Core.Services.BatchAssetS
{
    public class BatchAssetService : IBatchAsset
    {
        private readonly ApplicationDbContext _context;

        public BatchAssetService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<AssetBatch>> GetAllBatchAsset()
        {
            List<AssetBatch> result = null;
            try
            {
                result = await _context.assetBatches.ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }


        public async Task<int> addAsseBatch(List<AssetBatch> _assetBatch)
        {
            var info = _context.assetBatches.OrderByDescending(x => x.BatchId).FirstOrDefault();
            try
            {
                var tg = info.TagId+1; //3
                foreach (var ab in _assetBatch)
                {            
                    ab.TagId = tg;
                    ab.SerialNumber = info.SerialNumber;
                    _context.assetBatches.Add(ab);
                    await _context.SaveChangesAsync();
                    tg++;
                }
                return _assetBatch.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpAsseBatch(List<AssetBatch> assetBatch)
        {
            try
            {
                foreach (var ab in assetBatch)
                {
                    var existingAssetBatch = await _context.assetBatches.FirstOrDefaultAsync(x => x.BatchId == ab.BatchId);

                    if (existingAssetBatch != null)
                    {
                        existingAssetBatch.BatchId = ab.BatchId;
                        existingAssetBatch.BatchName = ab.BatchName;
                        existingAssetBatch.AssetName = ab.AssetName; 
                        existingAssetBatch.TagId = ab.TagId;
                        existingAssetBatch.SerialNumber = ab.SerialNumber;

                        _context.assetBatches.Add(ab);
                        await _context.SaveChangesAsync();
                    }
                }

                await _context.SaveChangesAsync();
                return assetBatch.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
