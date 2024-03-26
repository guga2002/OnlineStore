using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Data
{
    public class StoreContext:IdentityDbContext<Person>
    {
        public StoreContext(DbContextOptions<StoreContext>ops):base(ops)
        {      
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptDetail> ReceiptsDetails { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
