﻿namespace Cart.Application.Dto
{
    public class CartItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string Total { get; set; }

    }
}
