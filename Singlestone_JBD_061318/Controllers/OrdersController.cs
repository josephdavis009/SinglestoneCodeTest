using LiteDB;
using Singlestone_JBD_061318.Helpers;
using Singlestone_JBD_061318.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Singlestone_JBD_061318.Controllers
{
    public class OrdersController : Controller
    {
        [HttpGet]
        public JsonResult Get(int id = 0)
        {
            if (id != 0)
            {
                return new JsonResult() { Data = DBHelper.GetOrder(id), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult() { Data = DBHelper.GetAllOrders(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpPost]
        public JsonResult Create(Order newOrder)
        {
            Customer cust = DBHelper.GetCustomer(newOrder.CustomerId);

            if (cust == null)
            {
                cust = new Customer(newOrder.CustomerId);
                DBHelper.CreateCustomer(cust);
            }

            Uri baseAddress = new Uri(ConfigurationManager.AppSettings["ProductAPIUri"]);
            foreach (var item in newOrder.Items)
            {
                var request = HttpHelper.GetResponse(baseAddress, $"api/products/{item.ProductId}");
                if (request.IsSuccessStatusCode)
                {
                    item.Product = request.Content.ReadAsAsync<Product>().Result;
                }
            }

            var order = new Order
            {
                CustomerId = newOrder.CustomerId,
                Items = newOrder.Items
            };

            DBHelper.CreateOrder(order);
            if (cust.Orders == null)
            {
                cust.Orders = new List<Order>();
            }
            cust.Orders.Add(order);
            DBHelper.UpdateCustomer(cust);
            OrderReceipt receipt = new OrderReceipt(order);
            return new JsonResult() { Data = receipt, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


    }
}