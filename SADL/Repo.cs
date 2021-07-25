using System.Collections.Generic;
using System.Linq;
using SAModels;

namespace SADL
{
    public class Repo : IRepo
    {
        private StoreAppDBContext _context;
        public Repo(StoreAppDBContext p_context)
        {
            _context = p_context;
        }
        
        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.Select(customer => customer).ToList();
        }
        public Customer AddCustomer(Customer p_customer)
        {
            _context.Customers.Add(p_customer);
            _context.SaveChanges();
            return p_customer;
        }

        public Customer GetOneCustomer(string p_customerEmail)
        {
            return _context.Customers.FirstOrDefault(customer => customer.CustomerEmail == p_customerEmail);
        }

        public List<Order> GetCustomerOrders(int p_customerID)
        {
            return _context.Orders
                    .Where(order => order.CustomerID == p_customerID)
                    .Select(order => order).ToList();
        }

        public List<Order> GetStoreOrders(int p_storeID)
        {
            return _context.Orders
                    .Where(order => order.StoreFrontID == p_storeID)
                    .Select(order => order).ToList();
        }

        public Order PlaceOrder(Order p_order)
        {
            _context.Orders.Add(p_order);
            _context.SaveChanges();
            return p_order;
        }

        public List<StoreFront> GetAllStores()
        {
            return _context.Stores.Select(store => store).ToList();
        }

        public StoreFront AddStore(StoreFront p_store)
        {
            _context.Stores.Add(p_store);
            _context.SaveChanges();
            return p_store;
        }

        public double GetProductPrice(int p_productID)
        {
            var product = _context.Products.Find(p_productID);
            return product.ProductPrice;
        }

        public LineItem GetOneItem(int p_itemID)
        {
            return _context.LineItems.Find(p_itemID);
        }

        public StoreFront GetOneStore(int p_storeID)
        {
            return _context.Stores.Find(p_storeID);
        }

        public LineItem ReplenishInventory(int p_itemID, int p_amount)
        {
            var item = this.GetOneItem(p_itemID);

            item.Quantity += p_amount;

            _context.LineItems.Update(item);
            _context.SaveChanges();
            return item;
        }
        
        public List<LineItem> ViewInventory(int p_storeID)
        {
            return _context.LineItems
                    .Where(item => item.StoreFrontID == p_storeID)
                    .Select(item => item).ToList();
        }

        public Product AddProduct(Product p_product)
        {
            _context.Products.Add(p_product);
            _context.SaveChanges();
            return p_product;
        }
    }
}