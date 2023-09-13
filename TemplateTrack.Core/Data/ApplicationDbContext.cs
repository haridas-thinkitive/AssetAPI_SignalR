using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.DataAccess.Model;
using TemplateTrack.DataAccess.Model.Asset;
using TemplateTrack.DataAccess.Model.AssetCheckInInfo;
using TemplateTrack.DataAccess.Model.AssetCheckoutInfo;
using TemplateTrack.DataAccess.Model.AssetCheckOutRecord;
using TemplateTrack.DataAccess.Model.AssetHistoryInfo;
using TemplateTrack.DataAccess.Model.AssetManagement;
using TemplateTrack.DataAccess.Model.BatchAsset;
using TemplateTrack.DataAccess.Model.NewFolder;
using TemplateTrack.DataAccess.Model.Registration;
using TemplateTrack.DataAccess.Model.TraceInfo;
using TemplateTrack.DataAccess.Model.TrackingInfo;

namespace TemplateTrack.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AssetDetail> assetDetails { get; set; }

        public DbSet<RegistrationModel> registrationModels { get; set; }

        public DbSet<Asset> assets { get; set; }

        public DbSet<CheckOutRecord> checkOutRecords { get; set; }


        //05-09-2023

        public DbSet<AssetInfo> assetInfos { get; set; }
        public DbSet<AssetCheckInInfo> assetCheckInInfos{ get; set; }

        public DbSet<AssetCheckOutInfo> assetCheckOutInfos { get; set; }

        public DbSet<AssetHistory> assetHistories { get; set; }

        public DbSet<AssetBatch> assetBatches { get; set; }

        public DbSet<AssetTrackingInfo> assetTrackingInfos { get; set; }

        public DbSet<AssetTraceIfo> assetTraceIfos { get; set; }

    }
}
