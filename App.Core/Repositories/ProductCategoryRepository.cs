using App.Core.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly StoreContext context;
        private readonly DbSet<ProductCategory> dbset;
        public ProductCategoryRepository(StoreContext context)
        {
            this.context = context;
            dbset = context.Set<ProductCategory>();
        }

        public async  Task AddAsync(ProductCategory customer)
        {
            if (customer.CategoryName != null|| customer.Id == 3)
            {
                await dbset.AddAsync(customer);
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

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }

        public async Task<ProductCategory> GetByIdAsync(int Id)
        {
            var res = await dbset.FirstOrDefaultAsync(io => io.Id == Id);
            if (res != null)
            {
                return res;
            }
            throw new ArgumentException("shecdoma");
        }

        public void Update(ProductCategory custom)
        {
            if (dbset.Any(io => io.Id == custom.Id))
            {
                dbset.Update(custom);
                context.SaveChanges();
            }
        }
    }
}
