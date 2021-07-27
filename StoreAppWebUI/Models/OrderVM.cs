using SAModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAppWebUI.Models
{
    public class OrderVM
    {
        public OrderVM()
        { }

        public OrderVM(Order p_order)
        {
            OrderID = p_order.OrderID;
            StoreFrontID = p_order.StoreFrontID;
            CustomerID = p_order.CustomerID;
            OrderAddress = p_order.OrderAddress;
            OrderPrice = p_order.OrderPrice;
        }

        public int OrderID { get; set; }
        public int StoreFrontID { get; set; }
        public int CustomerID { get; set; }
        public string OrderAddress { get; set; }
        public double OrderPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
