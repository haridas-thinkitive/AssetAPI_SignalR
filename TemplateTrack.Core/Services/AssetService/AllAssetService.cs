using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.IAssetAll;
using TemplateTrack.DataAccess.Model.AssetCheckOutRecord;
using TemplateTrack.DataAccess.Model.AssetManagement;

namespace TemplateTrack.Core.Services.AssetService
{
    public class AllAssetService : IAllAsset
    {

        private readonly ApplicationDbContext _context;
        public AllAssetService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Asset>> GetAllAssets()
        {
            return  _context.assets.ToList();
        }

        public async Task<Asset> GetAssetById(int assetId)
        {

              var   result = await _context.assets.Where(x => x.AssetId == assetId).FirstOrDefaultAsync();
              return result;
        }

        public async Task<Asset> CheckInAsset(string barCodeNo)
        {
            var asset = await _context.assets.FirstOrDefaultAsync(x => x.Barcode == barCodeNo);

            if(asset.IsCheckedOut==true)
            {
                return asset;
            }
            else
            {
                if (asset != null)
                {
                    if (!asset.IsCheckedOut)
                    {
                        asset.IsCheckedOut = true; // Set the flag to true
                        var checkOutDetails = new CheckOutRecord
                        {
                            AssetId = asset.AssetId,
                            CheckOutRecordId = asset.AssetId,
                            CheckInDate = DateTime.Now,
                            CheckOutDate = DateTime.Now,
                            CheckedInBy = asset.AssetId.ToString(),
                            CheckedOutBy = "Pending"
                        };
                        _context.checkOutRecords.Add(checkOutDetails);
                        _context.SaveChanges(); // Save changes to the database
                    }
                    return asset;
                }
            }
            return null;


        }

    }
}
