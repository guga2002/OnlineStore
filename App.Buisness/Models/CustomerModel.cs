namespace Business.Models
{
    public class CustomerModel
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public DateTime BirthDate { get; set; }

        public int  DiscountValue { get; set; }

        public decimal ReceiptSum { get; set; }
    }
}
