using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTrack.DataAccess.Model.AssetHistoryInfo
{
    public class AssetHistory
    {
        [Key]
        public int AssetId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string CheckedInBy { get; set; }
        public string CheckedOutBy { get; set; }
        public bool CurrentCCStatus { get; set; }
        public string BarCode { get; set;}

        public bool IsCheckedInAsset { get; set; }
        public bool IsCheckedOutAsset { get; set; }

    }
}
