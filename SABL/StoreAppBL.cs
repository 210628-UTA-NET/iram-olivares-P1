using System.Collections.Generic;
using SAModels;
using SADL;

namespace SABL
{
    public class StoreAppBL : IStoreAppBL
    {
        private IRepo _repo;

        public StoreAppBL(IRepo p_repo)
        {
            _repo = p_repo;
        }

        public Customer AddCustomer(Customer p_customer)
        {
            return _repo.AddCustomer(p_customer);
        }

        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public Customer GetOneCustomer(string p_customerEmail)
        {
            return _repo.GetOneCustomer(p_customerEmail);
        }

        public List<Order> GetCustomerOrders(Customer p_customer)
        {
            return _repo.GetCustomerOrders(p_customer);
        }

        public List<Order> GetStoreOrders(StoreFront p_store)
        {
            return _repo.GetStoreOrders(p_store);
        }

        public Order PlaceOrder(Customer p_customer, StoreFront p_store, Order p_order)
        {
            return _repo.PlaceOrder(p_customer, p_store, p_order);
        }

        public List<StoreFront> GetAllStores()
        {
            return _repo.GetAllStores();
        }

        public StoreFront AddStore(StoreFront p_store)
        {
            return _repo.AddStore(p_store);
        }

        public double GetItemPrice(LineItem p_item)
        {
            return _repo.GetItemPrice(p_item);
        }

        public LineItem GetOneItem(string p_itemName, StoreFront p_store)
        {
            return _repo.GetOneItem(p_itemName, p_store);
        }

        public StoreFront GetOneStore(int p_storeID)
        {
            return _repo.GetOneStore(p_storeID);
        }
        public List<LineItem> ReplenishInventory(StoreFront p_store, LineItem p_item, int p_amount)
        {
            return _repo.ReplenishInventory(p_store, p_item, p_amount);
        }

        public List<LineItem> ViewInventory(StoreFront p_store)
        {
            return _repo.ViewInventory(p_store);
        }
    }
}