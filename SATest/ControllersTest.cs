using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SABL;
using SAModels;
using Moq;
using StoreAppWebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using StoreAppWebUI.Models;

namespace SATest
{
    public class ControllersTest
    {
        [Fact]
        public void ShouldGetAllStores()
        {
            var mockBL = new Mock<IStoreAppBL>();
            mockBL.Setup(storeappBL => storeappBL.GetAllStores()).Returns(
                new List<StoreFront>
                {
                    new StoreFront(){StoreName = "Test1 Store"},
                    new StoreFront(){StoreName = "Test2 Store"},
                    new StoreFront(){StoreName = "Test3 Store"}
                }
            );

            var storeFrontController = new StoreFrontController(mockBL.Object);

            var result = storeFrontController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<StoreFrontVM>>(viewResult.ViewData.Model);

            Assert.Equal(3, model.Count());
        }

        [Fact]
        public void ShouldGetAllCustomers()
        {
            var mockBL = new Mock<IStoreAppBL>();
            mockBL.Setup(storeAppBL => storeAppBL.GetAllCustomers()).Returns(
                new List<Customer>
                {
                    new Customer(){CustomerPhone = "5487956854"},
                    new Customer(){CustomerPhone = "2165487858"}
                }
            );

            var customerController = new CustomerController(mockBL.Object);

            var result = customerController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<CustomerVM>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }
    }
}
