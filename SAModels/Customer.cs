using System.Collections.Generic;

namespace SAModels
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public List<Order> CustomerOrders { get; set; }

        public override string ToString()
        {
            return $"Name: {CustomerName}\nAddress: {CustomerAddress}\nEmail: {CustomerEmail}\nPhone: {CustomerPhone}";
        }
    }
}