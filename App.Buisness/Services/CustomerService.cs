using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Entities;
using Data.Interfaces;
using System.ComponentModel.Design;

namespace Business.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly IUnitOfWork obj;
        private  readonly IMapper mapper;

        public CustomerService(IUnitOfWork obj, IMapper mapper)
        {
            this.obj =obj;
            this.mapper = mapper;
        }

        public async Task AddAsync(CustomerModel item)
        {
            if (item==null)
            {
                throw new MarketException("exct");
            }
            if(item.BirthDate>=DateTime.Now||item.BirthDate<=new DateTime(1890,1,1))
            {
                throw new MarketException("Tarigi");
            }
            if (!string.IsNullOrEmpty(item.Name) && !string.IsNullOrEmpty(item.Surname))
            {
                var mapped = mapper.Map<Customer>(item);

                await obj.CustomerRepository.AddAsync(mapped);
                await obj.SaveAsync();
            }
            else
            {
                throw new MarketException("shecdoma");
            }
        }

        public async Task DeleteAsync(int item)
        {
            try
            {
                if (item <= 0)
                {
                    throw new MarketException("shecdoma");
                }
                await  obj.CustomerRepository.DeleteByIdAsync(item);
                await obj.SaveAsync();
            }
            catch (Exception)
            {
                throw new MarketException("shecdoma");
            }
        }

        public async  Task<IEnumerable<CustomerModel>> GetAllAsync()
        {
            var res = await obj.CustomerRepository.GetAllAsync();
            if (res == null) throw new MarketException("Shecdoma");
            var mapped=mapper.Map<IEnumerable<CustomerModel>>(res);
            if (mapped == null) throw new MarketException("shecdoma");
            return mapped;
        }

        public async  Task<IEnumerable<CustomerModel>> GetCustomersByProductIdAsync(int id)
        {
            if (id <= 0) throw new MarketException("error");
            var res = await obj.CustomerRepository.GetAllAsync();
            if(res!=null)
            {
                var axal = res.Where(io => io.Receipts
                            .SelectMany(r => r.ReceiptDetails)
                            .Any(rd => rd.ProductId == id))
              .ToList();
                if (axal.Count()==0)
                {
                    throw new  MarketException("shecdoma");
                }
                var mapped = mapper.Map<IEnumerable<CustomerModel>>(axal);
                if (mapped == null) throw new MarketException("shecdoma");

                return mapped;
            }
            throw new MarketException("shecdoma");
        }

        public async Task UpdateAsync(CustomerModel item)
        {
           
            if (item.BirthDate >= DateTime.Now || item.BirthDate <= new DateTime(1890, 1, 1))
            {
                throw new MarketException("Tarigi");
            }
            if (string.IsNullOrEmpty(item.Name)||string.IsNullOrEmpty(item.Surname))
            {
                throw new MarketException("shecdoma");
            }
           var res= mapper.Map<Customer>(item);
            if (obj.CustomerRepository != null)
            {
                obj.CustomerRepository.Update(res);
                await obj.SaveAsync();
            }
        }
    }
}
