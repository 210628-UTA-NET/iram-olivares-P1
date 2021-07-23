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
            throw new System.NotImplementedException();
        }
        public Customer AddCustomer(Customer p_customer)
        {
            throw new System.NotImplementedException();
        }

        public Customer GetOneCustomer(string p_customerEmail)
        {
            throw new System.NotImplementedException();
        }

        public List<Order> GetCustomerOrders(Customer p_customer)
        {
            throw new System.NotImplementedException();
        }

        public List<Order> GetStoreOrders(StoreFront p_store)
        {
            throw new System.NotImplementedException();
        }

        public Order PlaceOrder(Customer p_customer, StoreFront p_store, Order p_order)
        {
            throw new System.NotImplementedException();
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

        public double GetItemPrice(Item p_item)
        {
            throw new System.NotImplementedException();
        }

        public LineItem GetOneItem(string p_itemName, StoreFront p_store)
        {
            throw new System.NotImplementedException();
        }

        public StoreFront GetOneStore(int p_storeID)
        {
            throw new System.NotImplementedException();
        }

        public List<LineItem> ReplenishInventory(StoreFront p_store, LineItem p_item, int p_amount)
        {
           throw new System.NotImplementedException();
        }
        
        public List<LineItem> ViewInventory(StoreFront p_store)
        {
            throw new System.NotImplementedException();
        }
    }
}