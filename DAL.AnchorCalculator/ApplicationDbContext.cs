using Core.AnchorCalculator.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AnchorCalculator
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Anchor> Anchors { get; set; }
        public DbSet<Material> Materials { get; set; }
        public override DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SetNullBehaviour(builder);
        }

        private static void SetNullBehaviour(ModelBuilder builder)
        {
            builder.Entity<Anchor>()
             .HasOne(b => b.Material)
             .WithMany(a => a.Anchors)
             .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Anchor>()
             .HasOne(b => b.User)
             .WithMany(a => a.Anchors)
             .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
