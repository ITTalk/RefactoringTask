using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PriceCalculator
{
    public class OrderPriceCalculator
    {
        public decimal CalculateOrderTotalPrice(Guid orderNumber, List<OrderItem> items, Client client, string OutputCurrencyCode)
        {
            if (items == null)
                return 0;

            if (items.Count == 0)
                return 0;

            var currencyValidator = new CurrencyValidator();
            if (!currencyValidator.IsValidCurrency(OutputCurrencyCode))
                throw new ArgumentException("Invalid currency.");

            decimal totalPrice = 0;

            // Calculate the total price of order
            foreach (var item in items)
            {
                totalPrice += item.Price;
            }

            // Apply discount for premium members
            if (client.IsPremiumMebmer == true)
            {
                var exchangeRateCalculator = new ExchangeRateCalculator();
                var orderCurrency = items[0].CurrencyCode;

                var totalPriceInPln = exchangeRateCalculator.CalculatePriceInCurrency(totalPrice, orderCurrency, OutputCurrencyCode);

                // Check if discount should be applied
                if (totalPriceInPln > 1500)
                {
                    Debug.WriteLine($"Applying premium member discount. Order {orderNumber}, Client {client.ClientId}");
                    totalPrice = totalPrice * 0.95m; // discount 5% 
                }
            }

            // additional discount for birthday
            if (DateTime.Now.Date == client.BirthDate.Date)
            {
                Debug.WriteLine($"Applying member birthday discount. Order {orderNumber}, Client {client.ClientId}");
                totalPrice = totalPrice * 0.98m; // discount 2%
            }

            return totalPrice;
        }
    }
}