﻿namespace Order.Api.Models
{
    using System.Collections.Generic;

    public class OrderInputDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardAuthCode { get; set; }
        public List<OrderItemInputDto> OrderItems { get; set; }
    }
}
