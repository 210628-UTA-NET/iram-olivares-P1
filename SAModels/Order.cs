using System.Collections.Generic;

namespace SAModels
{
    public class Order
    {
        public int OrderID { get; set; }
        public string OrderAddress { get; set; }
        public double OrderPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public override string ToString()
        {
            return $"Store Address: {OrderAddress}\nTotal Price: ${OrderPrice}";
        }
    }
}