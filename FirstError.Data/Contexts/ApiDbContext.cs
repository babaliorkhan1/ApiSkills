 using FirstApi.Core.Entities;
using FirstError.Core.Entities;
using FirstError.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Data.Contexts
{
    public class ApiDbContext:IdentityDbContext<IdentityUser>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Product> Products { get; set; }
        public DbSet<Log> Logs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //modelBuilder.ApplyConfigurationsFromAssembly()//bir defe yazirsan iki defe yazmiran ferqi budu 
            base.OnModelCreating(modelBuilder);
        }
       
    }
}
