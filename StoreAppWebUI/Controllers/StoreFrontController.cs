using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SABL;
using StoreAppWebUI.Models;
using SAModels;

namespace StoreAppWebUI.Controllers
{
    public class StoreFrontController : Controller
    {
        private IStoreAppBL _storeAppBL;
        public StoreFrontController(IStoreAppBL p_storeAppBL)
        {
            _storeAppBL = p_storeAppBL;
        }
        public IActionResult Index()
        {
            return View(
                _storeAppBL.GetAllStores()
                .Select(store => new StoreFrontVM(store))
                .ToList()
            );
        }

        public IActionResult AddStore()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStore(StoreFrontVM p_storeVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _storeAppBL.AddStore(new StoreFront
                        {
                            StoreName = p_storeVM.StoreName,
                            StoreAddress = p_storeVM.StoreAddress
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

        public IActionResult InspectStore(int p_storeID)
        {
            StoreFrontVM store = new StoreFrontVM(_storeAppBL.GetOneStore(p_storeID));
            store.LineItems = _storeAppBL.ViewInventory(p_storeID);
            store.Orders = _storeAppBL.GetStoreOrders(p_storeID);

            return View(store);
        }
    }
}
