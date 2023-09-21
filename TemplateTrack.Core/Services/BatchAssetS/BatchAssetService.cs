using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public BatchAssetService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

        //public async Task<string> BatchAsset(List<AssetBatch> _assetBatch)
        //{
        //    try
        //    {
        //       var ExistsRecord = await _context.assetBatches.FirstOrDefaultAsync();
        //       if (ExistsRecord != null)
        //       {
        //            // int batchSize = _configuration.GetValue<int>("BatchSize");
        //            if (_configuration.GetValue<int>("BatchSize") > 0 && _assetBatch.Count <= _configuration.GetValue<int>("BatchSize"))
        //            {
        //                var info = _context.assetBatches.OrderByDescending(x => x.BatchId).FirstOrDefault();

        //                var tg = info.TagId + 1; //3
        //                foreach (var ab in _assetBatch)
        //                {
        //                    ab.TagId = tg;
        //                    ab.SerialNumber = info.SerialNumber;
        //                    _context.assetBatches.Add(ab);
        //                    await _context.SaveChangesAsync();
        //                    tg++;
        //                }
        //                return "Record Added Successfully";
        //            }
        //            else
        //            {
        //                return "I am able to take only 50 records";
        //            }
        //       }
        //       else
        //        {
        //            return "Set Barcode As manually";
        //        }
        //    }catch (Exception ex)
        //    {
        //        throw ex ;
        //    }           
        //}

        public async Task<string> BatchAsset(List<AssetBatch> _assetBatch)
        {
            try
            {
                int batchSize = _configuration.GetValue<int>("BatchSize");

                if (batchSize <= 0)
                {
                    return "Batch size is not configured.";
                }

                if (_assetBatch == null || !_assetBatch.Any())
                {
                    return "No records to insert.";
                }

                // Calculate the number of batches required
                int totalRecords = _assetBatch.Count;
                int totalBatches = (totalRecords + batchSize - 1) / batchSize; //Calculate the batch division

                for (int batchNumber = 0; batchNumber < totalBatches; batchNumber++)
                {
                    var batch = _assetBatch.Skip(batchNumber * batchSize).Take(batchSize).ToList();

                    var info = _context.assetBatches.OrderByDescending(x => x.BatchId).FirstOrDefault();
                    var tg = info != null ? info.TagId + 1 : 1; 

                    foreach (var ab in batch)
                    {
                        ab.TagId = tg;
                        if (info?.SerialNumber != null)
                        {
                            ab.SerialNumber = info.SerialNumber;
                        }
                        else
                        {
                            ab.SerialNumber = 0;
                        }
                        _context.assetBatches.Add(ab);
                        tg++;
                    }
                    await _context.SaveChangesAsync();
                }
                return "Records added successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> AddBatch(List<BatchAssetInfo> _assetBatch)
        {
            try
            {
                int batchSize = _configuration.GetValue<int>("BatchSize");
                if (batchSize <= 0 || _assetBatch == null || !_assetBatch.Any())
                {
                    if (batchSize <= 0)
                        return "Batch size is not configured.";
                    else
                        return "No records to insert.";
                }
                int initialBatchType = 1;         
                int totalRecords = _assetBatch.Count; 
                for (int batchNumber = 0; batchNumber < (totalRecords + batchSize - 1) / batchSize; batchNumber++)
                {
                    var batch = _assetBatch.Skip(batchNumber * batchSize).Take(batchSize).ToList();
                    var info = _context.batchAssetInfos.OrderByDescending(x => x.BatchId).FirstOrDefault();
                    var tg = info != null ? info.TagId + 1 : 1;

                    foreach (var ab in batch)
                    {
                        ab.TagId = tg;
                        if (info?.SerialNumber != null)
                        {
                            ab.SerialNumber = info.SerialNumber;
                        }
                        else
                        {
                            ab.SerialNumber = 0;
                        }
                        ab.BatchType = initialBatchType.ToString(); // Set the batch type as a string
                        _context.batchAssetInfos.Add(ab);
                        tg++;
                    }
                    await _context.SaveChangesAsync();
                    initialBatchType++;
                }
                return "Records added successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Step 1 Without parallel Its Working 
        /// </summary>
        /// <param name="_assetBatch"></param>
        /// <returns></returns>
        //public async Task<string> AddBatchparallel(List<BatchAssetInfo> _assetBatch)
        //{
        //    try
        //    {
        //        int batchSize = _configuration.GetValue<int>("BatchSize");

        //        if (batchSize <= 0)
        //        {
        //            return "Batch size is not configured.";
        //        }

        //        if (_assetBatch == null || !_assetBatch.Any())
        //        {
        //            return "No records to insert.";
        //        }

        //        int initialBatchType = 1; // Set the initial batch type

        //        int totalRecords = _assetBatch.Count; // Calculate the number of batches required
        //        int totalBatches = (totalRecords + batchSize - 1) / batchSize; //Calculate the batch division

        //        for (int batchNumber = 0; batchNumber < totalBatches; batchNumber++)
        //        {
        //            var batch = _assetBatch.Skip(batchNumber * batchSize).Take(batchSize).ToList();

        //            var info = _context.batchAssetInfos.OrderByDescending(x => x.BatchId).FirstOrDefault();
        //            var tg = info != null ? info.TagId + 1 : 1;

        //            foreach (var ab in batch)
        //            {
        //                ab.TagId = tg;
        //                if (info?.SerialNumber != null)
        //                {
        //                    ab.SerialNumber = info.SerialNumber;
        //                }
        //                else
        //                {
        //                    ab.SerialNumber = 0;
        //                }
        //                ab.BatchType = initialBatchType.ToString(); // Set the batch type as a string
        //                _context.batchAssetInfos.Add(ab);
        //                tg++;
        //            }
        //            await _context.SaveChangesAsync();
        //            initialBatchType++;

        //        }
        //        return "Records added successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public async Task<string> AddBatchparallel(List<BatchAssetInfo> _assetBatch)
        //{
        //    try
        //    {
        //        int batchSize = _configuration.GetValue<int>("BatchSize");

        //        if (batchSize <= 0)
        //        {
        //            return "Batch size is not configured.";
        //        }

        //        if (_assetBatch == null || !_assetBatch.Any())
        //        {
        //            return "No records to insert.";
        //        }

        //        int initialBatchType = 1; // Set the initial batch type

        //        int totalRecords = _assetBatch.Count; // Calculate the number of batches required
        //        int totalBatches = (totalRecords + batchSize - 1) / batchSize; // Calculate the batch division

        //        await Task.Run(async () =>
        //        {
        //            for (int batchNumber = 0; batchNumber < totalBatches; batchNumber++)
        //            {
        //                var batch = _assetBatch.Skip(batchNumber * batchSize).Take(batchSize).ToList();

        //                var info = _context.batchAssetInfos.OrderByDescending(x => x.BatchId).FirstOrDefault();
        //                var tg = info != null ? info.TagId + 1 : 1;

        //                foreach (var ab in batch)
        //                {
        //                    ab.TagId = tg;
        //                    if (info?.SerialNumber != null)
        //                    {
        //                        ab.SerialNumber = info.SerialNumber;
        //                    }
        //                    else
        //                    {
        //                        ab.SerialNumber = 0;
        //                    }
        //                    ab.BatchType = initialBatchType.ToString(); // Set the batch type as a string
        //                    _context.batchAssetInfos.Add(ab);
        //                    tg++;
        //                }
        //                await _context.SaveChangesAsync();
        //                initialBatchType++;
        //            }
        //        });

        //        return "Records added successfully"; // Return a value outside the Task.Run
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<string> AddBatchparallel(List<BatchAssetInfo> _assetBatch)
        {
            try
            {
                int batchSize = _configuration.GetValue<int>("BatchSize");

                if (batchSize <= 0)
                {
                    return "Batch size is not configured.";
                }

                if (_assetBatch == null || !_assetBatch.Any())
                {
                    return "No records to insert.";
                }

                int initialBatchType = 1;

                int totalRecords = _assetBatch.Count; // Calculate the number of batches required

                int totalBatches = (totalRecords + batchSize - 1) / batchSize; // Calculate the batch division

                var batchProgress = new List<int>(); // Store the progress of each batch task

                await Task.Run(async () =>
                {
                    for (int batchNumber = 0; batchNumber < totalBatches; batchNumber++)
                    {
                        var batch = _assetBatch.Skip(batchNumber * batchSize).Take(batchSize).ToList();

                        var info = _context.batchAssetInfos.OrderByDescending(x => x.BatchId).FirstOrDefault();
                        var tg = info != null ? info.TagId + 1 : 1;

                        foreach (var ab in batch)
                        {
                            ab.TagId = tg;
                            if (info?.SerialNumber != null)
                            {
                                ab.SerialNumber = info.SerialNumber;
                            }
                            else
                            {
                                ab.SerialNumber = 0;
                            }
                            ab.BatchType = initialBatchType.ToString(); // Set the batch type as a string
                            _context.batchAssetInfos.Add(ab);
                            tg++;
                        }

                        // Log the batch number before starting to save
                        int currentBatchNumber = batchNumber;
                        batchProgress.Add(currentBatchNumber);
                        await _context.SaveChangesAsync();
                        batchProgress.Remove(currentBatchNumber);
                        initialBatchType++;
                    }
                });

                // Check if all batch tasks have completed
                while (batchProgress.Count > 0)
                {
                    // Continue waiting until all batch tasks are completed
                    await Task.Delay(1000); // Wait for a short interval before checking again
                }

                return "Records added successfully"; // Return a value outside the Task.Run
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }



}
