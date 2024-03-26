﻿namespace Business.Models
{
    public class ReceiptDetailModel
    {
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal DiscountUnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}
