using Business.Models;

namespace Business.Interfaces
{
    public interface IProductService:Icrud<ProductModel>
    {
        Task UpdateCategoryAsync(ProductCategoryModel mod);
        Task RemoveCategoryAsync(int a);
         Task AddCategoryAsync(ProductCategoryModel mod);
        Task<IEnumerable<ProductCategoryModel>> GetAllProductCategoriesAsync();
    }
}
