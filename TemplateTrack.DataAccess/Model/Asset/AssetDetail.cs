using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTrack.DataAccess.Model.Asset
{
    public class AssetDetail
    {
            public int Id { get; set; }
            public string AssetName { get; set; }
            public string AssetType { get; set; }
            public string SerialNumber { get; set; }
            public decimal PurchasePrice { get; set; }
            public DateTime PurchaseDate { get; set; }
            public string Location { get; set; }
            public string AssignedTo { get; set; }
            public bool IsInUse { get; set; }
            public DateTime LastMaintenanceDate { get; set; }
            public string Notes { get; set; }
    }
}

