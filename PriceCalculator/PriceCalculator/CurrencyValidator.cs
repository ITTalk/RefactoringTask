using System;
using System.Collections.Generic;
using System.Text;

namespace PriceCalculator
{
    public class CurrencyValidator
    {
        private List<string> validCurrencies = new List<string>
        {
            "PLN",
            "EUR",
            "USD"
        };

        public bool IsValidCurrency(string currencyCdoe)
        {
            if (validCurrencies.Contains(currencyCdoe))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
