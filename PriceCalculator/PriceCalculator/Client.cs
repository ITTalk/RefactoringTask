using System;
using System.Collections.Generic;
using System.Text;

namespace PriceCalculator
{
    public class Client
    {
        public Guid ClientId { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsPremiumMebmer { get; set; }
    }
}
