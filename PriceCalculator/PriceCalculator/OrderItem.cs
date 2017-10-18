using System;
using System.Collections.Generic;
using System.Text;

namespace PriceCalculator
{
    public class OrderItem
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string CurrencyCode { get; set; }
    }
}
