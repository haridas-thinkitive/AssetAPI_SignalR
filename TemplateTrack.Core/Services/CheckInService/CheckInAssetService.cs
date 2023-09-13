using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.CheckInAsset;
using TemplateTrack.DataAccess.Model.AssetCheckInInfo;
using TemplateTrack.DataAccess.Model.AssetCheckOutRecord;
using TemplateTrack.DataAccess.Model.AssetHistoryInfo;
using TemplateTrack.DataAccess.Model.AssetManagement;
using TemplateTrack.DataAccess.Model.BatchAsset;
using TemplateTrack.DataAccess.Model.NewFolder;

namespace TemplateTrack.Core.Services.CheckInService
{
    public class CheckInAssetService : IcheckInAsset
    {

        private readonly ApplicationDbContext _context;

        public CheckInAssetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AssetCheckInInfo>> getAllCheckInAsset()
        {
            List<AssetCheckInInfo> result = null;
            try
            {
                result = await _context.assetCheckInInfos.ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<AssetInfo> CheckInAsset(string barCodeNo)
        {
            try
            {
                var asset = await _context.assetInfos.FirstOrDefaultAsync(x => x.Barcode == barCodeNo);

                if (asset.IsCheckdOut == true)
                {
                    return asset;
                }
                else
                {
                    if (asset != null)
                    {
                        if (!asset.IsCheckdOut)
                        {
                            asset.IsCheckdIn = true; // Set the flag to true
                            var checkOutDetails = new AssetHistory
                            {
                                CheckInDate = DateTime.Now,
                                CheckOutDate = DateTime.Now,
                                CheckedInBy = "LNV-Team",
                                BarCode = barCodeNo,
                                CheckedOutBy = "Not Chcked Out",
                                CurrentCCStatus = true,
                                IsCheckedInAsset = asset.IsCheckdIn,
                                IsCheckedOutAsset = asset.IsCheckdOut,
                            };
                            _context.assetHistories.Add(checkOutDetails);
                            _context.SaveChanges(); // Save changes to the database
                        }
                        return asset;
                    }
                }
                return null;
            }catch(Exception ex)
            {
                throw ex;
            }


        }

        public async Task<AssetInfo> CheckOutAsset(string barCodeNo)
        {
            try
            {
                var asset = await _context.assetInfos.FirstOrDefaultAsync(x => x.Barcode == barCodeNo);

                if (asset.IsCheckdIn == false)
                {
                    return asset;
                }
                else
                {
                    if (asset != null)
                    {
                        var assetHistory = await _context.assetHistories.FirstOrDefaultAsync(x => x.BarCode == barCodeNo);

                        if(assetHistory != null)
                        {
                          assetHistory.IsCheckedOutAsset = true;
                          await _context.SaveChangesAsync();
                          return asset;
                        }
                        return null;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


    }
}
