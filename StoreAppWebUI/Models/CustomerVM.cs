using SAModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAppWebUI.Models
{
    public class CustomerVM
    {
        public CustomerVM()
        { }

        public CustomerVM(Customer p_customer)
        {
            CustomerID = p_customer.CustomerID;
            CustomerName = p_customer.CustomerName;
            CustomerAddress = p_customer.CustomerAddress;
            CustomerEmail = p_customer.CustomerEmail;
            CustomerPhone = String.Format("{0:(###) ###-####}", Int64.Parse(p_customer.CustomerPhone));
        }

        public int CustomerID { get; set; }
        [Required(ErrorMessage = "The Customer Name field is required")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "The Customer Address field is required")]
        public string CustomerAddress { get; set; }
        [Required(ErrorMessage = "The Customer Email field is required")]
        public string CustomerEmail { get; set; }
        [Required(ErrorMessage = "The Customer Phone field is required")]
        public string CustomerPhone { get; set; }
        public List<Order> CustomerOrders { get; set; }

    }
}
