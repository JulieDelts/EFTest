using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRelations.Models
{
    internal class Tag
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        public List<Brick> Bricks { get; set; } = new();
    }
}
