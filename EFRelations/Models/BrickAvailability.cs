using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFRelations.Models;

namespace EFRelations
{
    internal class BrickAvailability
    {
        public int Id { get; set; }
        public Vendor Vendor { get; set; } = new();
        public int VendorId { get; set; }
        public Brick Brick { get; set; } = new();
        public int BrickId { get; set; }
        public int AvailableAmount { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal PriceEur { get; set; }

    }
}
