using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Singlestone_JBD_061318.Models;
using System.Collections.Generic;

namespace Singlestone_JBD_061318.Tests
{
    [TestClass]
    public class OrderTesting
    {
        [TestMethod]
        public void ValidateItemSum()
        {
            double p1 = 0.33;
            int q1 = 10;
            Product testProduct = new Product() { Name = "Test", Category = "Test item", Id = "p1", Price = p1 };
            Item testItem = new Item() { Product = testProduct, Quantity = q1 };
            Assert.AreEqual((p1 * q1), testItem.Sum, 0, "Item sum is incorrect");
        }

        [TestMethod]
        public void ValidateOrderSum()
        {
            double p1 = 0.33;
            double p2 = 100.25;
            double p3 = 26.30;
            int q1 = 10;
            int q2 = 5;
            int q3 = 15;
            Product testProduct1 = new Product() { Name = "Test 1", Category = "Test item 1", Id = "p1", Price = p1 };
            Item testItem1 = new Item() { Product = testProduct1, Quantity = q1 };

            Product testProduct2 = new Product() { Name = "Test 2", Category = "Test item 2", Id = "p2", Price = p2 };
            Item testItem2 = new Item() { Product = testProduct2, Quantity = q2 };

            Product testProduct3 = new Product() { Name = "Test 3", Category = "Test item 3", Id = "p3", Price = p3 };
            Item testItem3 = new Item() { Product = testProduct3, Quantity = q3 };

            Order testOrder = new Order() { Id = 1, Items = new List<Item> { testItem1, testItem2, testItem3 } };

            Assert.AreEqual((p1*q1)+(p2*q2)+(p3*q3), testOrder.Sum , 0, "Order sum is incorrect");
        }
    }
}
