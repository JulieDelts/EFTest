using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    internal class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public int? Stars { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
    }
}
