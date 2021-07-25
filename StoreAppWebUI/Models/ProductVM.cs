using SAModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAppWebUI.Models
{
    public class ProductVM
    {
        public ProductVM()
        { }

        public ProductVM(Product p_product)
        {
            ProductName = p_product.ProductName;
            ProductPrice = p_product.ProductPrice;
            ProductDescription = p_product.ProductDescription;
        }

        public int ProductID;
        public string ProductName;
        public double ProductPrice;
        public string ProductDescription;
    }
}
