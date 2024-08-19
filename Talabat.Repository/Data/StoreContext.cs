using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Repository.Data.Config;

namespace Talabat.Repository.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///we Commented It Because DbContext Don't Have DbSets Like IdentityDbContext
            ///base.OnModelCreating(modelBuilder);

            ///we Can Use Another Way That Get All Configurations (Using Reflection). 
            ///modelBuilder.ApplyConfiguration(new ProductConfigurations());
            ///modelBuilder.ApplyConfiguration(new ProductBrandConfigurations());
            ///modelBuilder.ApplyConfiguration(new ProductCategoryConfigurations());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
