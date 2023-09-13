using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTrack.DataAccess.Model.AssetCheckOutRecord
{
    public class CheckOutRecord
    {
        [Key]
        public int Id { get; set; }
        public int CheckOutRecordId { get; set; }
        public int AssetId { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public string CheckedOutBy { get; set; }
        public string CheckedInBy { get; set; }
    }
}
