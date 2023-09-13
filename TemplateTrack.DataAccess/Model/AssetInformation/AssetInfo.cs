using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTrack.DataAccess.Model.NewFolder
{
    public class AssetInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }

        public string SerialNumber { get; set; }

        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }

        public string Manufacturer { get; set; }

        public string ModelNo { get; set; }

        public string Location { get; set; }

        public int AssignedUserId { get; set; }

        public int CategoryId { get; set; }

        public string Condition { get; set; }

        public DateTime? WarrantyExpirationDate { get; set; }

        public string MaintenanceSchedule { get; set; }

        public bool IsCheckdIn { get; set; }

        public bool IsCheckdOut { get; set; }


    }
}
