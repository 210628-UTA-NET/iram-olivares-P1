using System.Collections.Generic;
using SAModels;

namespace SABL
{
    public interface IStoreAppBL
    {
        /// <summary>
        /// AddCustomer takes in a previously created Customer Object and adds it to a database (or collection)
        /// Does not return anything since a Customer is only being added
        /// </summary>
        /// <param name="p_customer"> Customer object as an input </param>
        Customer AddCustomer(Customer p_customer);

        /// <summary>
        /// GetAllCustomers will retrieve the list of customers from the database
        /// Deserializes the included JSON file
        /// </summary>
        /// <returns> Returns the list of customers from the database </returns>
        List<Customer> GetAllCustomers();

        /// <summary>
        /// GetOneCustomer will search the database for a Customer
        /// </summary>
        /// <param name="p_customerEmail"> Takes an email string that will compare to all emails in the database </param>
        /// <returns> Will return a Customer if found, null otherwise </returns>
        Customer GetOneCustomer(string p_customerEmail);

        /// <summary>
        /// Allows a Customer to place an order at the specified store
        /// </summary>
        /// <param name="p_customer"> Customer input </param>
        /// <param name="p_store"> Store input </param>
        /// <returns> Returns the Order if successful, null otherwise </returns>
        Order PlaceOrder(Order p_order);

        /// <summary>
        /// Allows user to view a Customer's orders
        /// </summary>
        /// <param name="p_customer"> Customer input </param>
        /// <returns> Returns the List of Orders if successful, null otherwise</returns>
        List<Order> GetCustomerOrders(int p_customerID);

        /// <summary>
        /// Allows user to view a Store's orders
        /// </summary>
        /// <param name="p_store"> Store input </param>
        /// <returns> Returns the List of Orders if successful, null otherwise </returns>
        List<Order> GetStoreOrders(int p_storeID);

        /// <summary>
        /// Allows user to recieve a list of all available stores
        /// </summary>
        /// <returns> Will return a list of available stores </returns>
        List<StoreFront> GetAllStores();

        /// <summary>
        /// Retrieves a store from the database
        /// </summary>
        /// <param name="p_storeID"> ID as a parameter to extract the correct store </param>
        /// <returns> Will return a store from the database </returns>
        StoreFront GetOneStore(int p_storeID);

        /// <summary>
        /// Adds a store to the database
        /// </summary>
        /// <param name="p_store"> Takes is a store as a parameter </param>
        /// <returns> Returns the newly added store if successful </returns>
        StoreFront AddStore(StoreFront p_store);

        /// <summary>
        /// Allows the user to view a store's inventory
        /// </summary>
        /// <param name="p_storeID"> Takes in a store's ID as a parameter </param>
        /// <returns> Will return a list of the store's available items </returns>
        List<LineItem> ViewInventory(int p_storeID);

        /// <summary>
        /// Retrieves an Item from a store's inventory
        /// </summary>
        /// <param name="p_itemName"> Name of the item requested </param>
        /// <param name="p_store"> Specified store to search through </param>
        /// <returns> Searches through the database for an item and store match </returns>
        LineItem GetOneItem(int p_itemID);

        /// <summary>
        /// Allows the user to replenish an item in a store. Will also allow for removal of items if a Customer places an order
        /// </summary>
        /// <param name="p_store"> Takes in a store that needs resupply </param>
        /// <param name="p_item"> Takes in the item to be replenished or removed </param>
        /// <param name="p_amount"> The number of items to be added or removed </param>
        /// <returns> Will return the newly updated inventory </returns>
        LineItem ReplenishInventory(int p_itemID, int p_amount);

        /// <summary>
        /// Get the price of a specified item
        /// </summary>
        /// <param name="p_item"> Takes in a line item as an input </param>
        /// <returns> Returns the item price in double format </returns>
        double GetProductPrice(int p_productID);

        /// <summary>
        /// Add a product to the database
        /// </summary>
        /// <param name="p_product"> Product to be added </param>
        /// <returns> Returns the newly added product </returns>
        Product AddProduct(Product p_product);
    }
}