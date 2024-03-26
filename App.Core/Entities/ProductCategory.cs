using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("ProductCategories")]
    [Index(nameof(CategoryName))]
    public class ProductCategory:AbstractEntity
    {

        [Column("Category_Name")]
        public string? CategoryName { get; set; }

        public List<Product> Products { get; set; }
    }
}
