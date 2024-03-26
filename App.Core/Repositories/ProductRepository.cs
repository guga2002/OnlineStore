using App.Core.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;
        private readonly DbSet<Product> dbset;

        public ProductRepository(StoreContext context)
        {
            this.context = context;
            dbset = context.Set<Product>();
        }

        public async  Task AddAsync(Product customer)
        {
            if ( customer.ProductName!=null ||customer.Id==3)
            {
                await dbset.AddAsync(new Product()
                {
                    Price=customer.Price,
                    ProductCategoryId=customer.ProductCategoryId,
                    ProductName=customer.ProductName
                });
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteByIdAsync(int Id)
        {
            var res = await dbset.FirstOrDefaultAsync(io => io.Id == Id);
            if (res != null)
            {
                dbset.Remove(res);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }


        public async  Task<Product> GetByIdAsync(int Id)
        {
            var res = await dbset.FirstOrDefaultAsync(io => io.Id == Id);
            if (res != null)
            {
                return res;
            }
            throw new ArgumentException("shecdoma");
        }

        public void Update(Product custom)
        {
            var db = dbset
        .Include(io => io.Category)
        .Include(io => io.ReceiptDetails)
        .FirstOrDefault(io => io.Id == custom.Id);

            if (db != null && db.Category != null && custom.Category != null)
            {
                db.Price = custom.Price;
                db.ProductName = custom.ProductName;

                db.Category.CategoryName = custom.Category.CategoryName;
                db.ProductCategoryId = custom.ProductCategoryId;

                context.SaveChanges();
            }

        }
    }
}
 