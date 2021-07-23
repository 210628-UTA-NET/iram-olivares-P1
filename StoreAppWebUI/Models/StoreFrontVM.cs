using SAModels;
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

        [Required]
        public string StoreName { get; set; }
        [Required]
        public string StoreAddress { get; set; }

    }
}