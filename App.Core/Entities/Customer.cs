using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Customers")]
    public class Customer:AbstractEntity
    {
        [ForeignKey("person")]
        public string PersonId { get; set; }
        public decimal DiscountValue { get; set; }

        public DateTime OperationDate { get; set; }
        public  Person Person{ get; set; }
        public List<Receipt>? Receipts { get; set; }
    }
}
