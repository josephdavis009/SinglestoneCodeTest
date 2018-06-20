using LiteDB;
using Singlestone_JBD_061318.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Singlestone_JBD_061318.Helpers
{
    public static class DBHelper
    {
        private static LiteDatabase db = new LiteDatabase(@ConfigurationManager.AppSettings["DbContext"]);
        public static Order GetOrder(int id)
        {
            return db.GetCollection<Order>("orders").Find(s => s.Id == id).FirstOrDefault();
        }

        public static List<Order> GetAllOrders()
        {
            return db.GetCollection<Order>("orders").FindAll().OrderBy(s => s.CustomerId).OrderBy(s => s.Id).ToList();
        }

        public static Customer GetCustomer(string custId)
        {
            return db.GetCollection<Customer>("customers").Include(s => s.Orders).Find(s => s.CustomerId == custId).FirstOrDefault();
        }

        public static List<Customer> GetAllCustomers()
        {
            return db.GetCollection<Customer>("customers").Include(s => s.Orders).FindAll().ToList();
        }

        public static List<Order> GetCustomerOrders(string custId)
        {
            return db.GetCollection<Order>("orders").Find(s => s.CustomerId == custId).OrderBy(s => s.Id).ToList();
        }

        public static void CreateCustomer(Customer cust)
        {
            var customers = db.GetCollection<Customer>("customers");
            customers.Insert(cust);
        }

        public static void CreateOrder(Order order)
        {
            var orders = db.GetCollection<Order>("orders");
            orders.Insert(order);
        }

        public static void UpdateCustomer(Customer customer)
        {
            var customers = db.GetCollection<Customer>("customers");
            customers.Update(customer);
        }
    }
}