using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Receipts")]
    public class Receipt:AbstractEntity
    {
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Column("Operation_Date")]
        public DateTime OperationDate { get; set; }
        [Column("Is_Checked_Out")]
        public bool IsCheckedOut { get; set; }
        public Customer? Customer { get; set; }

        public List<ReceiptDetail>? ReceiptDetails { get; set; }
    }
}
