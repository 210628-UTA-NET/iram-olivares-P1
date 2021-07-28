using System.Collections.Generic;
using SAModels;
using SADL;

namespace SABL
{
    public class StoreAppBL : IStoreAppBL
    {
        private readonly IRepo _repo;

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

        public Customer GetOneCustomer(int p_customerID)
        {
            return _repo.GetOneCustomer(p_customerID);
        }

        public List<Order> GetCustomerOrders(int p_customerID)
        {
            return _repo.GetCustomerOrders(p_customerID);
        }

        public List<Order> GetStoreOrders(int p_storeID)
        {
            return _repo.GetStoreOrders(p_storeID);
        }

        public Order PlaceOrder(Order p_order)
        {
            return _repo.PlaceOrder(p_order);
        }

        public List<StoreFront> GetAllStores()
        {
            return _repo.GetAllStores();
        }

        public StoreFront AddStore(StoreFront p_store)
        {
            return _repo.AddStore(p_store);
        }

        public double GetProductPrice(int p_productID)
        {
            return _repo.GetProductPrice(p_productID);
        }

        public Product GetOneItem(int p_itemID)
        {
            return _repo.GetOneItem(p_itemID);
        }

        public StoreFront GetOneStore(int p_storeID)
        {
            return _repo.GetOneStore(p_storeID);
        }
        public LineItem ReplenishInventory(int p_itemID, int p_amount)
        {
            return _repo.ReplenishInventory(p_itemID, p_amount);
        }

        public List<LineItem> ViewInventory(int p_storeID)
        {
            return _repo.ViewInventory(p_storeID);
        }
    }
}