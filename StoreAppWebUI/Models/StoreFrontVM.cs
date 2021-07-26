using SAModels;
using SABL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreAppWebUI.Models
{
    public class StoreFrontVM
    {
        public StoreFrontVM()
        { }

        public StoreFrontVM(StoreFront p_store)
        {
            StoreFrontID = p_store.StoreFrontID;
            StoreName = p_store.StoreName;
            StoreAddress = p_store.StoreAddress;
        }

        public int StoreFrontID { get; set; }

        [Required(ErrorMessage = "The Store Name field is required")]
        public string StoreName { get; set; }
        [Required(ErrorMessage = "The Store Address field is required")]
        public string StoreAddress { get; set; }
        public List<LineItem> LineItems;
        public List<Order> Orders;

    }
}