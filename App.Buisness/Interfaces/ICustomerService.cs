using Business.Models;

namespace Business.Interfaces
{
    public interface ICustomerService:Icrud<CustomerModel>
    {
        Task<IEnumerable<CustomerModel>> GetCustomersByProductIdAsync(int id);
    }
}
