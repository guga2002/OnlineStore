using Data.Entities;

namespace Data.Interfaces
{
    public interface IReceiptDetailRepository : ICrudRep<ReceiptDetail>
    {
        void Delete(ReceiptDetail receiptDetail);
    }
}
