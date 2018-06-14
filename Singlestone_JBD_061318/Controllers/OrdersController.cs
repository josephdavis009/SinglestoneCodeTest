using LiteDB;
using Singlestone_JBD_061318.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Singlestone_JBD_061318.Controllers
{
    public class OrdersController : Controller
    {
        [HttpGet]
        public JsonResult Get(string id)
        {
            using (var db = new LiteDatabase(@"Order.db"))
            {
                if(!string.IsNullOrWhiteSpace(id))
                {
                    var order = db.GetCollection<Order>("orders").Find(s => s.Id == Int32.Parse(id)).FirstOrDefault();
                    return new JsonResult() { Data = order, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                var orders = db.GetCollection<Order>("orders").FindAll().OrderBy(s => s.CustomerId).OrderBy(s => s.Id);
                return new JsonResult() { Data = orders, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult Create(Order newOrder)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://petstoreapp.azurewebsites.net/")
            };

            foreach (var item in newOrder.Items)
            {
                var request = client.GetAsync($"api/products/{item.ProductId}").Result;
                if (request.IsSuccessStatusCode)
                {
                    item.Product = request.Content.ReadAsAsync<Product>().Result;
                }
            }

            using (var db = new LiteDatabase(@"Order.db"))
            {
                var orders = db.GetCollection<Order>("orders");
                var order = new Order
                {
                    CustomerId = newOrder.CustomerId,
                    Items = newOrder.Items
                };

                orders.Insert(order);

                OrderReceipt receipt = new OrderReceipt(order);
                return new JsonResult() { Data = receipt, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        

    }
}