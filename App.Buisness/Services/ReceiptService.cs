using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Business.Validation;

namespace Business.Services
{
    public class ReceiptService: IReceiptService
    {
        private readonly IUnitOfWork obj;
        private readonly IMapper mapper;

        public ReceiptService(IUnitOfWork obj, IMapper mapper)
        {
            this.obj = obj;
            this.mapper = mapper;
        }

        public async  Task AddAsync(ReceiptModel item)
        {
            if(item!=null)
            {
                var mapped = mapper.Map<Receipt>(item);
                if(mapped!=null)
                {
                    await obj.ReceiptRepository.AddAsync(mapped);
                   await  obj.SaveAsync();
                }
                else
                {
                    throw new MarketException("kide");
                }
            }
            else
            {
                throw new MarketException("kide");
            }
        }

        public async Task AddProductAsync(int productId, int receiptId, int quantity)
        {
            var receipt = await obj.ReceiptRepository.GetByIdAsync(receiptId);
            if (receipt == null)
            {
                throw new MarketException("shecdoma");
            }

            if (obj.ProductRepository != null)
            {
                var product = await obj.ProductRepository.GetByIdAsync(productId);

                if (product == null)
                {
                    throw new MarketException("no product exist");
                }
                ReceiptDetail details = new ReceiptDetail()
                {
                    ProductId = product.Id,
                    ReceiptId = receipt.Id,
                    UnitPrice = product.Price,
                };
                await obj.ReceiptDetailRepository.AddAsync(details);
                await obj.SaveAsync();
            }
            else
            {
                foreach (var item in receipt.ReceiptDetails)
                {
                    item.Quantity += quantity;
                }
                await obj.SaveAsync();
            }

        }

        public async Task CheckOutAsync(int id)
        {
           var res=await obj.ReceiptRepository.GetByIdAsync(id);
            res.IsCheckedOut = true;
            await obj.SaveAsync();
        }

        public async Task DeleteAsync(int receiptId)
        {
            var receipt = await obj.ReceiptRepository.GetByIdAsync(receiptId);

            foreach (var item in receipt.ReceiptDetails)
            {
             obj.ReceiptDetailRepository.Delete(item);
            }
            await obj.ReceiptRepository.DeleteByIdAsync(receipt.Id);
            await obj.SaveAsync();
        }

        public async  Task<IEnumerable<ReceiptModel>> GetAllAsync()
        {
            var res= await obj.ReceiptRepository.GetAllAsync();
            if (res == null) throw new MarketException("ahaa");
            var mapped = mapper.Map<IEnumerable<ReceiptModel>>(res);
            if (mapped != null)
            {
                return mapped;
            }
            else
            {
                throw new MarketException("gassworaa");
            }
        }


        public async  Task<IEnumerable<ReceiptModel>> GetReceiptsByPeriodAsync(DateTime start, DateTime end)
        {
            var rec = await obj.ReceiptRepository.GetAllAsync();
            if (rec == null) throw new MarketException("shecdoma brat");
            var sab = rec.Where(io => io.OperationDate >= start && io.OperationDate <= end).ToList();
            if (sab == null) throw new MarketException("kide");
            List<ReceiptModel> Receipts = new List<ReceiptModel>();
            foreach (var item in sab)
            {
                ReceiptModel mod = new ReceiptModel()
                {
                    IsCheckedOut = item.IsCheckedOut,
                    OperationDate = item.OperationDate,
                    CustomerId = item.CustomerId,
                };
                Receipts.Add(mod);
            }
            return Receipts;
        }

        public async  Task<decimal> GetReceiptSum(int id)
        {
            var res=await obj.ReceiptDetailRepository.GetByIdAsync(id);

            if (res != null)
            {
                return res.Quantity * res.UnitPrice;
            }
            return 0;
        }

        public async Task RemoveProductAsync(int productId, int id, int quantity)
        {
            var res = await obj.ReceiptRepository.GetByIdAsync(productId);
            if (res != null && res.ReceiptDetails != null)
            {

                var detail = res.ReceiptDetails.FirstOrDefault(rd => rd.ProductId == id);

                if (detail != null)
                {

                    if (obj.ReceiptDetailRepository != null)
                    {
                        detail.Quantity -= quantity;

                        if (detail.Quantity <= 0)
                        {
                            obj.ReceiptDetailRepository.Delete(detail);
                        }
                        await obj.SaveAsync();
                    }
                }

            }
        }

        public async Task<decimal> ToPayAsync(int id)
        {
            decimal shedeg = 0;
            var res = await obj.ReceiptRepository.GetByIdAsync(id);
            var rec=res.ReceiptDetails.ToList();
            foreach (var item in rec)
            {
                decimal temp = 0;
            temp += item.DiscountUnitPrice*item.Quantity;
                shedeg += temp;
           }
            return shedeg;
        }

        public Task UpdateAsync(ReceiptModel item)
        {
           if(item==null)
            {
                throw new MarketException("exp");
            }
            var mapped = mapper.Map<Receipt>(item);
                obj.ReceiptRepository.Update(mapped);
                obj.SaveAsync();
            return Task.CompletedTask;
        }
    }
}
