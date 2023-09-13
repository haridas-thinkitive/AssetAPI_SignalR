using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTrack.DataAccess.Model.TraceInfo
{
    public class AssetTraceIfo
    {
        [Key]
        public int Id { get; set; }

        public string BarCode { get; set; }
        public string Name { get; set; }
        public string landMark { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
