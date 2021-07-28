using System.Collections.Generic;
using System.Linq;
using SAModels;

namespace SADL
{
    public class Repo : IRepo
    {
        private readonly StoreAppDBContext _context;
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

        public Customer GetOneCustomer(int p_customerID)
        {
            return _context.Customers.Find(p_customerID);
        }

        public List<Order> GetCustomerOrders(int p_customerID)
        {
            var orders = _context.Orders
                    .Where(order => order.CustomerID == p_customerID)
                    .Select(order => order).ToList();

            foreach (Order order in orders)
            {
                order.OrderItems = _context.OrderItems
                                    .Where(item => item.OrderID == order.OrderID)
                                    .Select(item => item).ToList();
                
                foreach(OrderItem item in order.OrderItems)
                {
                    item.Product = this.GetOneItem(item.ProductID);
                }
            }

            return orders;
        }

        public List<Order> GetStoreOrders(int p_storeID)
        {
            var orders = _context.Orders
                    .Where(order => order.StoreFrontID == p_storeID)
                    .Select(order => order).ToList();

            foreach (Order order in orders)
            {
                order.OrderItems = _context.OrderItems
                                    .Where(item => item.OrderID == order.OrderID)
                                    .Select(item => item).ToList();
            }

            return orders;
        }

        public Order PlaceOrder(Order p_order)
        {
            foreach(OrderItem item in p_order.OrderItems)
            {
                item.Product = null;
                item.OrderID = p_order.OrderID;
                _context.OrderItems.Add(item);
            }
            _context.Orders.Add(p_order);
            _context.SaveChanges();
            return p_order;
        }

        public List<OrderItem> AddOrderItemsToList(List<OrderItem> p_orderItems)
        {
            foreach(OrderItem item in p_orderItems)
            {
                item.Product = null;
                _context.OrderItems.Add(item);
            }
            _context.SaveChanges();
            return p_orderItems;
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

        public Product GetOneItem(int p_itemID)
        {
            return _context.Products.Find(p_itemID);
        }

        public StoreFront GetOneStore(int p_storeID)
        {
            return _context.Stores.Find(p_storeID);
        }

        public LineItem ReplenishInventory(int p_itemID, int p_amount)
        {
            var item = _context.LineItems.Find(p_itemID);

            item.Quantity += p_amount;

            _context.LineItems.Update(item);
            _context.SaveChanges();
            return item;
        }
        
        public List<LineItem> ViewInventory(int p_storeID)
        {
            var inventory = _context.LineItems
                    .Where(item => item.StoreFrontID == p_storeID)
                    .Select(item => item).OrderBy(item => item.ProductID).ToList();

            foreach (LineItem item in inventory)
            {
                item.Product = this.GetOneItem(item.ProductID);
            }
            
            return inventory;
        }

        public Product AddProduct(Product p_product)
        {
            _context.Products.Add(p_product);
            _context.SaveChanges();
            return p_product;
        }
    }
}