using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PriceCalculator.Tests
{
    [TestClass]
    public class OrderPriceCalculatorTests
    {
        [TestMethod]
        public void ShouldApplyPremiumMemberDiscountForOrdersAbove1500PLN()
        {
            var priceCalculator = new OrderPriceCalculator();
            var orderItems = new List<OrderItem>() 
            {
                new OrderItem() { ItemName = "test item 1", CurrencyCode = "PLN", Price = 1450 },
                new OrderItem() { ItemName = "test item 2", CurrencyCode = "PLN", Price = 100 }
            };
            var client = new Client() { BirthDate = new DateTime(1999, 1, 1), IsPremiumMebmer = true };

            var totalPrice = priceCalculator.CalculateOrderTotalPrice(Guid.NewGuid(), orderItems, client, "PLN");

            Assert.AreEqual((orderItems[0].Price + orderItems[1].Price) * 0.95m, totalPrice);
        }

        [TestMethod]
        public void ShouldNotApplyPremiumMemberDiscountForOrdersUnderOrEqualTo1500PLN()
        {
            var priceCalculator = new OrderPriceCalculator();
            var orderItems = new List<OrderItem>()
            {
                new OrderItem() { ItemName = "test item 1", CurrencyCode = "PLN", Price = 1400 },
                new OrderItem() { ItemName = "test item 2", CurrencyCode = "PLN", Price = 100 }
            };
            var client = new Client() { BirthDate = new DateTime(1999, 1, 1), IsPremiumMebmer = true };

            var totalPrice = priceCalculator.CalculateOrderTotalPrice(Guid.NewGuid(), orderItems, client, "PLN");

            Assert.AreEqual((orderItems[0].Price + orderItems[1].Price), totalPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowOnInvalidOutputCurrency()
        {
            var priceCalculator = new OrderPriceCalculator();
            var orderItems = new List<OrderItem>()
            {
                new OrderItem() { ItemName = "test item 1", CurrencyCode = "PLN", Price = 1400 },
                new OrderItem() { ItemName = "test item 2", CurrencyCode = "PLN", Price = 100 }
            };
            var client = new Client() { BirthDate = new DateTime(1999, 1, 1), IsPremiumMebmer = true };

            priceCalculator.CalculateOrderTotalPrice(Guid.NewGuid(), orderItems, client, "AUD");
        }
    }
}
