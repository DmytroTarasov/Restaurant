using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Portion> Portions { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<Dish>()
                .HasOne(d => d.Category)
                .WithMany(c => c.Dishes)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Portion>()
                .HasOne(p => p.Dish)
                .WithMany(d => d.Portions)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Dish>()
                .HasMany(d => d.Ingredients)
                .WithMany(i => i.Dishes);

            builder.Entity<Portion>()
                .HasMany(p => p.Orders)
                .WithMany(o => o.Portions);
        }
    }
}