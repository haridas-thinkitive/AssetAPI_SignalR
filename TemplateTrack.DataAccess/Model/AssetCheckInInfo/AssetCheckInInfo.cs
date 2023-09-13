using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTrack.DataAccess.Model.AssetCheckInInfo
{
    public class AssetCheckInInfo
    {
        [Key]
        public int Id { get; set; }           
        public int AssetId { get; set; }     
        public DateTime CheckInDate { get; set; } 
        public string CheckedInBy { get; set; }
        public bool IsCheckedOut { get; set; }
        public bool IsCheckedIn { get; set; }

        public string CheckInCode { get; set; }
    }
}
