namespace Business.Models
{
    public class ReceiptModel
    {
        public int CustomerId { get; set; }

        public bool IsCheckedOut { get; set; }

        public DateTime OperationDate { get; set; }
    }
}
