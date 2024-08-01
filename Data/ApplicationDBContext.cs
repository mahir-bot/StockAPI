using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetAPI.Data
{
    public class ApplicationDBContext: IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Portfolio>().HasKey(x => new { x.AppUserId, x.StockId });

            modelBuilder.Entity<Portfolio>().HasOne<AppUser>(x => x.AppUser).WithMany(x => x.Portfolios).HasForeignKey(x => x.AppUserId);
            modelBuilder.Entity<Portfolio>().HasOne<Stock>(x => x.Stock).WithMany(x => x.Portfolios).HasForeignKey(x => x.StockId);

            List<IdentityRole> roles =
            [
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            ];
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}