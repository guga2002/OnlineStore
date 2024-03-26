namespace Business.Models
{
    public class ProductModel
    {
        public string? ProductName { get; set; }

        public int ProductCategoryId { get; set; }

        public string? CategoryName { get; set; }

        public decimal Price { get; set; }

        public List<int>? ReceiptDetailIds { get; set; }
    }
}
