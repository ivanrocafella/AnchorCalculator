using Core.AnchorCalculator.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AnchorCalculator
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Anchor> Anchors { get; set; } = null!;
        public DbSet<Material> Materials { get; set; } = null!;

    }
}
