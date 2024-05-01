using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRelations.Models
{
    enum Colour
    {
        Black,
        White,
        Red,
        Yellow,
        Orange,
        Green
    }
    internal class Brick
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public Colour? Colour { get; set; }
        public List<Tag> Tags { get; set; } = new();
        public List<BrickAvailability> Availability { get; set; } = new();
    }
}
