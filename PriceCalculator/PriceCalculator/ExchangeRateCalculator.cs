using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceCalculator
{
    public class ExchangeRateCalculator
    {
        private class ExchangeRate
        {
            public string From { get; set; }
            public string To { get; set; }
            public decimal RateValue { get; set; }
        }

        private List<ExchangeRate> rates = new List<ExchangeRate>()
        {
            new ExchangeRate { From = "PLN", To = "EUR", RateValue = 1 },
            new ExchangeRate { From = "PLN", To = "USD", RateValue = 1 },

            new ExchangeRate { From = "EUR", To = "PLN", RateValue = 1 },
            new ExchangeRate { From = "EUR", To = "USD", RateValue = 1 },

            new ExchangeRate { From = "USD", To = "PLN", RateValue = 1 },
            new ExchangeRate { From = "USD", To = "EUR", RateValue = 1 }
        };

        public decimal CalculatePriceInCurrency(decimal price, string fromCurrency, string toCurrency)
        {
            if (price == 0)
                return 0;

            if (price < 0)
                throw new ArgumentException("Invalid price");

            if (fromCurrency == toCurrency)
                return price;

            var currencyValidator = new CurrencyValidator();
            if (!currencyValidator.IsValidCurrency(fromCurrency) || !currencyValidator.IsValidCurrency(toCurrency))
                throw new ArgumentException("Invalid currency.");

            var rate = rates.FirstOrDefault(r => r.From == fromCurrency && r.To == toCurrency);
            if (rate == null)
                throw new InvalidOperationException($"Rate conversion not possible from {fromCurrency} to {toCurrency}.");

            return price * rate.RateValue;
        }
    }
}
