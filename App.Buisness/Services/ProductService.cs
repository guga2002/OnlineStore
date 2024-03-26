using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Business.Validation;
using Data.Entities;

namespace Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork obj;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork obje, IMapper mapper)
        {
            this.obj = obje;
            this.mapper = mapper;
        }

        public async Task AddAsync(ProductModel item)
        {
            

            if (string.IsNullOrEmpty(item.ProductName))
            {
                throw new MarketException("shecdomaa");
            }
            if(item.Price<0)
            {
                throw new MarketException("shecdoma");
            }
            if (obj.ProductRepository != null)
            {
                var mapped = mapper.Map<Product>(item);
                await obj.ProductRepository.AddAsync(mapped);
                await obj.SaveAsync();
            }
        }

        public async Task AddCategoryAsync(ProductCategoryModel mod)
        {
            if (string.IsNullOrEmpty(mod.CategoryName)|| mod == null) throw new MarketException("shecdomaaa");
            var mapped = mapper.Map<ProductCategory>(mod);
            if (mapped == null) throw new MarketException("kide");

            await obj.ProductCategoryRepository.AddAsync(mapped);
            await obj.SaveAsync();
        }

        public async Task DeleteAsync(int item)
        {
            if (item <= 0) throw new MarketException("kide");
            await obj.ProductRepository.DeleteByIdAsync(item);
           await obj.SaveAsync();
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            var res = await obj.ProductRepository.GetAllAsync();
            var mapped = mapper.Map<IEnumerable<ProductModel>>(res);
            return mapped;
        }

        public async  Task<IEnumerable<ProductCategoryModel>> GetAllProductCategoriesAsync()
        {
            var res = await obj.ProductCategoryRepository.GetAllAsync();
            if (res == null) throw new MarketException("shecdoma");
            var mapped = mapper.Map<IEnumerable<ProductCategoryModel>>(res);
            return mapped;
        }

       

        public async  Task<ProductModel> GetByIdAsync(int Id)
        {
            var res = await obj.ProductRepository.GetByIdAsync(Id);
            var mapped = mapper.Map<ProductModel>(res);
            return mapped;
        }

        public async Task RemoveCategoryAsync(int a)
        {
           await  obj.ProductCategoryRepository.DeleteByIdAsync(a);
           await obj.SaveAsync();
        }

        public async  Task UpdateAsync(ProductModel item)
        {
            if (item == null || string.IsNullOrEmpty(item.ProductName))
            {
                throw new MarketException("ProductModel cannot be null and ProductName cannot be empty.");
            }

            var mappedProduct = mapper.Map<Product>(item);

            if (mappedProduct == null)
            {
                throw new MarketException("Failed to map ProductModel to Product.");
            }
            obj.ProductRepository.Update(mappedProduct);
            await obj.SaveAsync();

        }

        public async  Task UpdateCategoryAsync(ProductCategoryModel mod)
        {
            if (string.IsNullOrEmpty(mod.CategoryName) || mod == null) throw new MarketException("kide");
            var mapped=mapper.Map<ProductCategory>(mod);
            obj.ProductCategoryRepository.Update(mapped);
            await obj.SaveAsync(); 
        }
    }
}
