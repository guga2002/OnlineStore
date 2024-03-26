using App.Core.Data;
using Data.Interfaces;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(StoreContext db,
       ICustomerRepository customerRepository,
       IProductRepository productRepository,
       IProductCategoryRepository productCategoryRepository,
       IReceiptRepository receiptRepository,
       IReceiptDetailRepository receiptDetailRepository)
        {
            CustomerRepository = customerRepository;
            ProductRepository = productRepository;
            ProductCategoryRepository = productCategoryRepository;
            ReceiptRepository = receiptRepository;
            this.ReceiptDetailRepository = receiptDetailRepository;
            _dbContext = db;
        }
        private readonly StoreContext _dbContext;
        public ICustomerRepository CustomerRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductCategoryRepository ProductCategoryRepository { get; }
        public IReceiptRepository ReceiptRepository { get; }
        public IReceiptDetailRepository ReceiptDetailRepository { get; }

        public async Task SaveAsync()
        {
           await  _dbContext.SaveChangesAsync();
        }
    }
}
