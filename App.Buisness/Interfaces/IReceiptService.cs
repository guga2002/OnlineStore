using Business.Models;

namespace Business.Interfaces
{
    public interface IReceiptService:Icrud<ReceiptModel>
    {
        Task<decimal> ToPayAsync(int id);
       Task RemoveProductAsync(int id, int productId, int quantity);
        Task CheckOutAsync(int id);
       Task AddProductAsync(int id, int productId, int quantity);
      Task<IEnumerable<ReceiptModel>> GetReceiptsByPeriodAsync(DateTime start, DateTime end);
      Task<decimal> GetReceiptSum(int id);
    }
}
