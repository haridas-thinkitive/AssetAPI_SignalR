using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.AssetOperation;
using TemplateTrack.DataAccess.Model.Asset;
using TemplateTrack.DataAccess.Model.AssetManagement;
using TemplateTrack.DataAccess.Model.Hubs;
using TemplateTrack.DataAccess.Model.NewFolder;

namespace TemplateTrack.Core.Services.AssetOperationService
{
    public class AssetOperationServices : IAssetOperation
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<TableDataHub> _hubContext;


        public AssetOperationServices(ApplicationDbContext context , IHubContext<TableDataHub>  hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<List<AssetInfo>> GetAllassetInfo()
        {
            List<AssetInfo> result = null;
            try
            {
                result = await _context.assetInfos.ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }

        //public async Task<int> addAsseInfo(AssetInfo _assetInfos)
        //{
        //    int result = 0;

        //    try
        //    {
        //        AssetInfo objasset = new AssetInfo
        //        {
        //            Id = _assetInfos.Id,
        //            Name = _assetInfos.Name,
        //            Barcode = _assetInfos.Barcode,
        //            SerialNumber = _assetInfos.SerialNumber,
        //            PurchaseDate = _assetInfos.PurchaseDate,
        //            PurchasePrice = _assetInfos.PurchasePrice,
        //            Manufacturer = _assetInfos.Manufacturer,
        //            ModelNo = _assetInfos.ModelNo,
        //            Location = _assetInfos.Location,
        //            AssignedUserId = _assetInfos.AssignedUserId,
        //            CategoryId = _assetInfos.CategoryId,
        //            Condition = _assetInfos.Condition,
        //            WarrantyExpirationDate = _assetInfos.WarrantyExpirationDate,
        //            MaintenanceSchedule = _assetInfos.MaintenanceSchedule,
        //        };

        //        _context.assetInfos.Add(objasset);
        //        _context.SaveChanges();

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<int> addAsseInfo(AssetInfo _assetInfo)
        {
            try
            {
                var newAssetInfo = new AssetInfo
                {
                    Name = _assetInfo.Name,
                    Barcode = _assetInfo.Barcode,
                    SerialNumber = _assetInfo.SerialNumber,
                    PurchaseDate = _assetInfo.PurchaseDate,
                    PurchasePrice = _assetInfo.PurchasePrice,
                    Manufacturer = _assetInfo.Manufacturer,
                    ModelNo = _assetInfo.ModelNo,
                    Location = _assetInfo.Location,
                    AssignedUserId = _assetInfo.AssignedUserId,
                    CategoryId = _assetInfo.CategoryId,
                    Condition = _assetInfo.Condition,
                    WarrantyExpirationDate = _assetInfo.WarrantyExpirationDate,
                    MaintenanceSchedule = _assetInfo.MaintenanceSchedule
                };

                    _context.assetInfos.Add(newAssetInfo);
                await _hubContext.Clients.All.SendAsync("ReceiveTableData", _assetInfo);
                await _context.SaveChangesAsync();

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<int> EditAsset(AssetInfo assetInfos, int id)
        //{
        //    var result = 1;

        //    try
        //    {
        //        var IfExist = await _context.assetInfos.FindAsync(id);

        //        if (IfExist != null)
        //        {
        //            IfExist.Name = assetInfos.Name;
        //            IfExist.Barcode = assetInfos.Barcode;
        //            IfExist.SerialNumber = assetInfos.SerialNumber;
        //            IfExist.PurchasePrice = assetInfos.PurchasePrice;
        //            IfExist.PurchaseDate = assetInfos.PurchaseDate;
        //            IfExist.Manufacturer = assetInfos.Manufacturer;
        //            IfExist.ModelNo = assetInfos.ModelNo;
        //            IfExist.Location = assetInfos.Location;
        //            IfExist.AssignedUserId = assetInfos.AssignedUserId;
        //            IfExist.CategoryId = assetInfos.CategoryId;
        //            IfExist.Condition = assetInfos.Condition;
        //            IfExist.WarrantyExpirationDate = assetInfos.WarrantyExpirationDate;
        //            IfExist.MaintenanceSchedule = assetInfos.MaintenanceSchedule;
        //            IfExist.IsCheckdIn = assetInfos.IsCheckdIn;
        //            IfExist.IsCheckdOut = assetInfos.IsCheckdOut;

        //            result = await _context.SaveChangesAsync();
        //            return result;
        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<int> EditAsset(AssetInfo assetInfos, int id)
        {
            int result = 1;
            try
            {

                var IfExists = await _context.assetInfos.Where(x => x.Id == assetInfos.Id).FirstOrDefaultAsync();

                if (IfExists != null)
                {
                    _context.Entry(IfExists).State = EntityState.Modified;
                    _context.Entry(IfExists).CurrentValues.SetValues(assetInfos);

                    await _context.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteAsset(int id)
        {
            try
            {
                var assetToDelete = await _context.assetInfos.FindAsync(id);
                if (assetToDelete != null)
                {
                    _context.assetInfos.Remove(assetToDelete);
                    await _context.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }catch(Exception ex) 
            {
                throw ex;
            }
        }

        public async Task<AssetInfo> GetById(int id)
        {
            try
            {
                if(id !=0)
                {
                    var IfExist = await _context.assetInfos.Where(x => x.Id == id).FirstOrDefaultAsync();

                    if (IfExist != null)
                    {
                        return IfExist;
                    }
                    return null;
                }
                return null;
            }catch(Exception ex)
            { 
                throw ex;
            }
        }
    }
}
