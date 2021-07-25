using SAModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAppWebUI.Models
{
    public class LineItemVM
    {
        public LineItemVM()
        { }

        public LineItemVM(LineItem p_item)
        {
            Product = p_item.Product;
            Quantity = p_item.Quantity;
        }

        public int LineItemID;
        public Product Product;
        public int Quantity;

    }
}
