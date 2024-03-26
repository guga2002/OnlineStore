using App.Core.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly StoreContext context;
        private readonly DbSet<Customer> dbset;

        public CustomerRepository(StoreContext context)
        {
            this.context = context;
            dbset = context.Set<Customer>();
        }

        public async Task AddAsync(Customer customer)
        {
            await dbset.AddAsync(customer);
            await context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int Id)
        {
            var res = await  dbset.FirstOrDefaultAsync(io => io.Id == Id);
            if (res != null)
            {
                dbset.Remove(res);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }


        public async Task<Customer> GetByIdAsync(int Id)
        {
            var res = await dbset.FirstOrDefaultAsync(io => io.Id == Id);
            if(res!=null)
            {
                return res;
            }
            throw new ArgumentException("bad");
        }

        public void Update(Customer custom)
        {
            var user=dbset.Where(io => io.Id ==custom.Id).Include(io=>io.Person).FirstOrDefault();
            if (user != null&&custom.Person!=null)
            {
                user.Person = new Person();
                user.Person.Name = custom.Person.Name;
                user.Person.Surname = custom.Person.Surname;
                user.Person.BirthDate = custom.Person.BirthDate;
                user.DiscountValue = custom.DiscountValue;
                user.OperationDate = custom.OperationDate;
                dbset.Update(user);
                context.SaveChanges();
            }
        }
    }
}
