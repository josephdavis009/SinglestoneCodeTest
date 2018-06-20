using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Singlestone_JBD_061318.Models
{
    public class Order
    {
        [BsonId]
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public List<Item> Items { get; set; }
        public double Sum
        {
            get { return this.Items.Sum(s => s.Sum); }
        }
    }

    public class Item
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Sum
        {
            get { return ((this.Product?.Price ?? 0.0) * this.Quantity); }
        }
    }

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double? Price { get; set; }
    }

    public class OrderReceipt
    {
        public OrderReceipt (Order order)
        {
            this.OrderId = order.Id;
            this.CustomerId = order.CustomerId;
            this.Total = $"${order.Sum}";
        }
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public string Total { get; set; }
    }
}