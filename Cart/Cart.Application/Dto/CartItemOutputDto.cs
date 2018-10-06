﻿namespace Cart.Application.Dto
{
    public class CartItemOutputDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; }
        public int Unit { get; set; }
        public string PictureUrl { get; set; }
        public string Total { get; set; }

    }
}
