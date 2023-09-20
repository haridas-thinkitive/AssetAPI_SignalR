using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTrack.DataAccess.Model.BatchAsset
{
    public class BatchAssetInfo
    {
        [Key]
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public string AssetName { get; set; }
        public int TagId { get; set; }
        public int SerialNumber { get; set; }
        public string BatchType { get; set; }

    }
}
