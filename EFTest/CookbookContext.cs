using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFTest
{
    internal class CookbookContext: DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public CookbookContext(DbContextOptions<CookbookContext> options): base(options)
        {}
    }
}
