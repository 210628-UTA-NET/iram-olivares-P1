using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SABL;
using SAModels;
using StoreAppWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAppWebUI.Controllers
{
    public class CustomerController : Controller
    {
        private IStoreAppBL _storeAppBL;
        public CustomerController(IStoreAppBL p_storeAppBL)
        {
            _storeAppBL = p_storeAppBL;
        }

        public IActionResult Index()
        {
            return View(
                _storeAppBL.GetAllCustomers()
                .Select(customer => new CustomerVM(customer))
                .ToList()
            );
        }

        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerVM p_customerVM)
        {
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
            return View();
        }
        public IActionResult InspectCustomer(int p_customerID)
        {
            CustomerVM customer = new CustomerVM(_storeAppBL.GetOneCustomer(p_customerID));
            customer.CustomerOrders = _storeAppBL.GetCustomerOrders(p_customerID);

            return View(customer);
        }

        public IActionResult ChooseStore(int p_customerID)
        {
            List<StoreFrontVM> stores = _storeAppBL.GetAllStores().Select(store => new StoreFrontVM(store)).ToList();
            ViewData["customerID"] = p_customerID;
            return View(stores);
        }

        [HttpPost]
        public IActionResult ChosenStore(int p_storeID, int p_customerID)
        {

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

            }
            else
            {
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

            TempData["cart"] = JsonConvert.SerializeObject(cart);
            TempData["customer"] = JsonConvert.SerializeObject(customer);
            TempData["store"] = JsonConvert.SerializeObject(store);

            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToOrder(int p_storeID, int p_customerID, int p_itemID, int p_amount)
        {
            OrderVM cart = JsonConvert.DeserializeObject<OrderVM>(TempData["cart"].ToString());
            CustomerVM customer = JsonConvert.DeserializeObject<CustomerVM>(TempData["customer"].ToString());
            StoreFrontVM store = JsonConvert.DeserializeObject<StoreFrontVM>(TempData["store"].ToString());

            LineItem findMe = store.LineItems.Find(item => item.LineItemID == p_itemID);

            if (p_amount <= findMe.Quantity)
            {
                OrderItem itemAdded = new OrderItem()
                {
                    Quantity = p_amount,
                    ProductID = findMe.ProductID,
                    OrderID = cart.OrderID,
                    Product = _storeAppBL.GetOneItem(findMe.ProductID)
                };

                if (cart.OrderItems == null)
                {
                    cart.OrderItems = new List<OrderItem> { itemAdded };
                    findMe.Quantity -= p_amount;
                    cart.OrderPrice += p_amount * itemAdded.Product.ProductPrice;
                }

                else if (!cart.OrderItems.Exists(item => item.ProductID == itemAdded.ProductID))
                {
                    cart.OrderItems.Add(itemAdded);
                    findMe.Quantity -= p_amount;
                    cart.OrderPrice += p_amount * itemAdded.Product.ProductPrice;
                }
            }

            TempData["cart"] = JsonConvert.SerializeObject(cart);
            TempData["customer"] = JsonConvert.SerializeObject(customer);
            TempData["store"] = JsonConvert.SerializeObject(store);

            return RedirectToAction(nameof(AddToOrder), new { p_storeID, p_customerID });
        }

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            OrderVM cart = JsonConvert.DeserializeObject<OrderVM>(TempData["cart"].ToString());

            Order checkout = new Order()
            {
                OrderAddress = cart.OrderAddress,
                OrderPrice = cart.OrderPrice,
                CustomerID = cart.CustomerID,
                StoreFrontID = cart.StoreFrontID,
                OrderItems = cart.OrderItems
            };

            checkout = _storeAppBL.PlaceOrder(checkout);

            return RedirectToAction(nameof(InspectCustomer), new { p_customerID = cart.CustomerID });
        }
    }
}
