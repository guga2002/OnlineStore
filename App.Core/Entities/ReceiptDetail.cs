using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("ReceiptDetails")]
    [Index(nameof(DiscountUnitPrice))]
    [Index(nameof(UnitPrice))]
    public class ReceiptDetail:AbstractEntity
    {
        [ForeignKey("Receipt")]
        public int ReceiptId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal DiscountUnitPrice { get; set; }

        public int Quantity { get; set; }

        public Receipt? Receipt { get; set; }

        public Product? Product { get; set; }
    }
}
