using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Singlestone_JBD_061318.Models
{
    public class Customer
    {
        public Customer()
        {
            //Generate some sort of new customer id
            CustomerId = Guid.NewGuid().ToString();
        }
        public Customer(string id)
        {
            CustomerId = id;
        }
        public int Id { get; set; }
        public string CustomerId { get; set; }
        [BsonRef("orders")]
        public List<Order> Orders { get; set; }
    }
}