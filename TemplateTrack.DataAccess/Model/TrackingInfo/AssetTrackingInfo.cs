using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTrack.DataAccess.Model.TrackingInfo
{
    public class AssetTrackingInfo
    {
        [Key]
        public int Id { get; set; }
        public int TrackingId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Location { get; set; }
        public bool IsCheckedOut { get; set; }
        public bool IsCheckedIn { get; set; }
        public string BarCode { get; set; }
      

    }
}
