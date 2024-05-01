using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    internal class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(50)]
        public string UnitOfMeasure { get; set; } = string.Empty;
        [Column(TypeName = "decimal(5,2)")]
        public decimal Amount { get; set; }
        public Dish? Dish { get; set; }
        public int DishId { get; set; }
    }
}
