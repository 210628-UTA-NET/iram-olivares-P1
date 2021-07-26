using Microsoft.AspNetCore.Mvc;
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
    }
}
