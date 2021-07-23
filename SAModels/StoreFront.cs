using System.Collections.Generic;

namespace SAModels
{
    public class StoreFront
    {
        public int StoreFrontID { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public List<LineItem> LineItems { get; set; }
        public List<Order> StoreOrders { get; set; }

        public override string ToString()
        {
            return $"ID: {StoreFrontID}\nName: {StoreName}\nAddress: {StoreAddress}";
        }
    }
}