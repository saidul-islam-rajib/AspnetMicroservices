﻿namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public decimal ProductId { get; set; }
        public decimal ProductName { get; set; }
    }
}
