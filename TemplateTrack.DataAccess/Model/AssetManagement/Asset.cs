using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTrack.DataAccess.Model.AssetManagement
{
    public class Asset
    {
        [Key]
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string Barcode { get; set; }
        public bool IsCheckedOut { get; set; }
    }
}
