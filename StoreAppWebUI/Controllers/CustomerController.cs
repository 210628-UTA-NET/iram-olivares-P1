using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SABL;
using SAModels;
using Serilog;
using StoreAppWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAppWebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IStoreAppBL _storeAppBL;
        public CustomerController(IStoreAppBL p_storeAppBL)
        {
            _storeAppBL = p_storeAppBL;
        }

        public IActionResult Index()
        {
            Log.Information("Visited Customer Homepage");
            return View(
                _storeAppBL.GetAllCustomers()
                .Select(customer => new CustomerVM(customer))
                .ToList()
            );
        }

        public IActionResult AddCustomer()
        {
            Log.Information("Visited Add Customer Interface - Who wants to buy some Tortillas?");
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerVM p_customerVM)
        {
            Log.Information("Adding Customer...");
            try
            {
                if (ModelState.IsValid)
                {
                    _storeAppBL.AddCustomer(new Customer
                    {
                        CustomerName = p_customerVM.CustomerName,
                        CustomerAddress = p_customerVM.CustomerAddress,
                        CustomerEmail = p_customerVM.CustomerEmail,
                        CustomerPhone = p_customerVM.CustomerPhone
                    }
                    );

                    // Return the Index of this Controller
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                return View();
            }
            Log.Information("Customer Added!");
            return View();
        }
        public IActionResult InspectCustomer(int p_customerID)
        {
            CustomerVM customer = new CustomerVM(_storeAppBL.GetOneCustomer(p_customerID));
            customer.CustomerOrders = _storeAppBL.GetCustomerOrders(p_customerID);

            Log.Information("Inspecting Customer - I wonder what they have ordered?");

            return View(customer);
        }

        public IActionResult ChooseStore(int p_customerID)
        {
            List<StoreFrontVM> stores = _storeAppBL.GetAllStores().Select(store => new StoreFrontVM(store)).ToList();
            ViewData["customerID"] = p_customerID;
            TempData.Clear();

            Log.Information("Choosing a Store to Order From");

            return View(stores);
        }

        [HttpPost]
        public IActionResult ChosenStore(int p_storeID, int p_customerID)
        {
            Log.Information("Store Chosen!");

            return RedirectToAction(nameof(AddToOrder), new { p_storeID, p_customerID});
        }

        public IActionResult AddToOrder(int p_storeID, int p_customerID)
        {
            OrderVM cart;
            CustomerVM customer;
            StoreFrontVM store;

            if (TempData.Count != 0)
            {
                cart = JsonConvert.DeserializeObject<OrderVM>(TempData["cart"].ToString());
                customer = JsonConvert.DeserializeObject<CustomerVM>(TempData["customer"].ToString());
                store = JsonConvert.DeserializeObject<StoreFrontVM>(TempData["store"].ToString());

                Log.Information("Fetching Temp Data");
            }
            else
            {
                Log.Information("Temp Data is empty! Making a new set");

                customer = new CustomerVM(_storeAppBL.GetOneCustomer(p_customerID));

                store = new StoreFrontVM(_storeAppBL.GetOneStore(p_storeID));
                store.LineItems = _storeAppBL.ViewInventory(p_storeID);

                cart = new OrderVM()
                {
                    CustomerID = p_customerID,
                    StoreFrontID = p_storeID,
                    OrderAddress = customer.CustomerAddress
                };
            }

            ViewData["customer"] = customer;
            ViewData["store"] = store;

            Log.Information("Saving Temp Data");

            TempData["cart"] = JsonConvert.SerializeObject(cart);
            TempData["customer"] = JsonConvert.SerializeObject(customer);
            TempData["store"] = JsonConvert.SerializeObject(store);

            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToOrder(int p_storeID, int p_customerID, int p_itemID, int p_amount)
        {
            
            Log.Information("Fetching Temporary Data");

            OrderVM cart = JsonConvert.DeserializeObject<OrderVM>(TempData["cart"].ToString());
            CustomerVM customer = JsonConvert.DeserializeObject<CustomerVM>(TempData["customer"].ToString());
            StoreFrontVM store = JsonConvert.DeserializeObject<StoreFrontVM>(TempData["store"].ToString());

            LineItem findMe = store.LineItems.Find(item => item.LineItemID == p_itemID);

            Log.Information("Attempting to add to cart");

            if (p_amount <= findMe.Quantity)
            {
                Log.Information("We have enough items!");

                OrderItem itemAdded = new OrderItem()
                {
                    Quantity = p_amount,
                    ProductID = findMe.ProductID,
                    OrderID = cart.OrderID,
                    Product = _storeAppBL.GetOneItem(findMe.ProductID)
                };

                if (cart.OrderItems == null)
                {
                    Log.Information("Empty cart! I'll make one for you");

                    cart.OrderItems = new List<OrderItem> { itemAdded };
                    findMe.Quantity -= p_amount;
                    cart.OrderPrice += p_amount * itemAdded.Product.ProductPrice;
                }

                else if (!cart.OrderItems.Exists(item => item.ProductID == itemAdded.ProductID))
                {
                    Log.Information("Adding to your cart");

                    cart.OrderItems.Add(itemAdded);
                    findMe.Quantity -= p_amount;
                    cart.OrderPrice += p_amount * itemAdded.Product.ProductPrice;
                }
            }

            Log.Information("Saving Temp Data");

            TempData["cart"] = JsonConvert.SerializeObject(cart);
            TempData["customer"] = JsonConvert.SerializeObject(customer);
            TempData["store"] = JsonConvert.SerializeObject(store);

            return RedirectToAction(nameof(AddToOrder), new { p_storeID, p_customerID });
        }

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            Log.Information("Placing Order");

            OrderVM cart = JsonConvert.DeserializeObject<OrderVM>(TempData["cart"].ToString());

            Order cartFix = new Order()
            {
                OrderAddress = cart.OrderAddress,
                OrderPrice = cart.OrderPrice,
                CustomerID = cart.CustomerID,
                StoreFrontID = cart.StoreFrontID,
                OrderItems = cart.OrderItems
            };

            Order checkout = _storeAppBL.PlaceOrder(cartFix);

            Log.Information("Order Placed!");

            return RedirectToAction(nameof(InspectCustomer), new { p_customerID = cart.CustomerID });
        }
    }
}
