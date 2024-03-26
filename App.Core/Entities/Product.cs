using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Products")]
    [Index(nameof(ProductName))]
    public class Product:AbstractEntity
    {
        [ForeignKey("Category")]
        public int ProductCategoryId { get; set; }

        [Column("ProductName")]
        public string? ProductName { get; set; }

        [Column("Product_price")]
        public decimal Price { get; set; }

        public ProductCategory? Category { get; set; }

        public List<ReceiptDetail>? ReceiptDetails { get; set; }
    }
}
