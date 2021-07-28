using SAModels;
using StoreAppWebUI.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SATest
{
    public class ViewModelValidationTest
    {
        [Fact]
        public void ValidCustomerTest()
        {
            Customer customer = new Customer() { CustomerPhone = "5555555555"};

            CustomerVM customerVM = new CustomerVM(customer);

            Assert.Equal("(555) 555-5555", customerVM.CustomerPhone);
        }
    }
}
