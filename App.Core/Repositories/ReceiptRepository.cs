using App.Core.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly StoreContext context;
        private readonly DbSet<Receipt> dbset;
        public ReceiptRepository(StoreContext context)
        {
            this.context = context;
            dbset = context.Set<Receipt>();
        }

        public async Task AddAsync(Receipt customer)
        {
            await dbset.AddAsync(customer);
            await context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int Id)
        {
            var res = await dbset.Where(io => io.Id == Id).FirstOrDefaultAsync();
                dbset.Remove(res??throw new ArgumentException("is null"));
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Receipt>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }

        public async  Task<Receipt> GetByIdAsync(int Id)
        {
            var res = await dbset.FirstOrDefaultAsync(io => io.Id == Id);
            if (res != null)
            {
                return res;
            }
            throw new ArgumentException("Errorrs");
        }

        public void Update(Receipt custom)
        {
            if (dbset.Any(io => io.Id == custom.Id))
            {
                dbset.Update(custom);
                context.SaveChanges();
            }
        }
    }
}
