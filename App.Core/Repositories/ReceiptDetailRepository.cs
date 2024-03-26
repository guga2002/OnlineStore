using App.Core.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ReceiptDetailRepository : IReceiptDetailRepository
    {
        private readonly StoreContext context;
        private readonly DbSet<ReceiptDetail> dbset; 

        public ReceiptDetailRepository(StoreContext context)
        {
            this.context = context;
            dbset = context.Set<ReceiptDetail>();
        }

        public async Task AddAsync(ReceiptDetail customer)
        {
            await dbset.AddAsync(customer);
            await context.SaveChangesAsync();
        }

        public async  void Delete(ReceiptDetail receiptDetail)
        {
                dbset.Remove(receiptDetail);
                await context.SaveChangesAsync();
        }

        public  async Task DeleteByIdAsync(int Id)
        {

            var res = await dbset.FirstOrDefaultAsync(io => io.Id == Id);
            if (res != null)
            {
                dbset.Remove(res);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ReceiptDetail>> GetAllAsync()
        {

            return await dbset.ToListAsync();
        }


        public async Task<ReceiptDetail> GetByIdAsync(int Id)
        {
            var res = await dbset.FirstOrDefaultAsync(io => io.Id == Id);
            if (res != null)
            {
                return res;
            }
            throw new ArgumentException("error");
        }

        public void Update(ReceiptDetail custom)
        {
            if (dbset.Any(io => io.Id == custom.Id))
            {
                dbset.Update(custom);
                context.SaveChanges();
            }
        }
    }
}
