using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using SADL;
using SAModels;
using System.Collections.Generic;
using System.Linq;

namespace SATest
{
    public class RepoTest
    {
        private readonly DbContextOptions<StoreAppDBContext> _options;

        // This constructor will run before every test case
        public RepoTest()
        {
            // Where the in-memory database will be stored
            _options = new DbContextOptionsBuilder<StoreAppDBContext>().UseSqlite("Filename = SATest.db").Options;
            this.Seed();
        }

        // Resets the database when Seed is called so no tests interfere with eachother
        private void Seed()
        {
            // using block to close connection after method finishes so no interference between tests
            using (var context = new StoreAppDBContext(_options))
            {
                // Make sure database is deleted before each time a test is run
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Add Products
                Product product1 = new Product()
                {
                    ProductName = "Tortilla #1",
                    ProductPrice = 1.11,
                    ProductDescription = "1 Tortilla"
                };

                Product product2 = new Product()
                {
                    ProductName = "Tortilla #2",
                    ProductPrice = 2.22,
                    ProductDescription = "2 Tortillas"
                };

                Product product3 = new Product()
                {
                    ProductName = "Tortilla #3",
                    ProductPrice = 3.33,
                    ProductDescription = "3 Tortillas"
                };
                Product product4 = new Product()
                {
                    ProductName = "Tortilla #4",
                    ProductPrice = 4.44,
                    ProductDescription = "4 Tortillas"
                };

                // Add Orders
                Order order1 = new Order()
                {
                    CustomerID = 1,
                    StoreFrontID = 1,
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Product = product1,
                            Quantity = 10,
                        }
                    }
                };

                Order order2 = new Order()
                {
                    CustomerID = 2,
                    StoreFrontID = 2,
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Product = product3,
                            Quantity = 30
                        },
                        new OrderItem
                        {
                            Product = product4,
                            Quantity = 40
                        }
                    }
                };

                Order order3 = new Order()
                {
                    CustomerID = 2,
                    StoreFrontID = 2,
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Product = product3,
                            Quantity = 30
                        },
                        new OrderItem
                        {
                            Product = product4,
                            Quantity = 40
                        }
                    }
                };


                // Add Stores with inventories
                context.Stores.AddRange(
                    new StoreFront
                    {
                        StoreName = "1st Tortilla Shop",
                        StoreAddress = "111 Tortilla Road",
                        LineItems = new List<LineItem>()
                        {
                            new LineItem
                            {
                                Product = product1,
                                Quantity = 100
                            },
                            new LineItem
                            {
                                Product = product2,
                                Quantity = 200
                            }
                        },
                        StoreOrders = new List<Order>()
                        {
                            order1
                        }
                    },

                    new StoreFront
                    {
                        StoreName = "2nd Tortilla Store",
                        StoreAddress = "222 Tortilla Road",
                        LineItems = new List<LineItem>()
                        {
                            new LineItem
                            {
                                Product = product3,
                                Quantity = 300
                            },
                            new LineItem
                            {
                                Product = product4,
                                Quantity = 400
                            }
                        },
                        StoreOrders = new List<Order>()
                        {
                            order2,
                            order3
                        }
                    }
                );

                // Add Customers
                context.Customers.AddRange(
                    new Customer
                    {
                        CustomerName = "1st Customer",
                        CustomerAddress = "111 Customer Road",
                        CustomerEmail = "111@customer.com",
                        CustomerPhone = "0123456789",
                        CustomerOrders = new List<Order>(){
                            order1
                        }
                    },
                    new Customer
                    {
                        CustomerName = "2nd Customer",
                        CustomerAddress = "222 Customer Road",
                        CustomerEmail = "222@customer.com",
                        CustomerPhone = "0123456789",
                        CustomerOrders = new List<Order>()
                        {
                            order2,
                            order3
                        }
                    }
                );

                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAllStoresTest()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                // Arrange
                IRepo repo = new Repo(context);
                List<StoreFront> stores;

                // Act
                stores = repo.GetAllStores();

                // Assert
                Assert.NotNull(stores);
                Assert.Equal(2, stores.Count);

            }
        }

        [Fact]
        public void GetOneStoreTest()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepo repo = new Repo(context);
                int findMe = 2;

                StoreFront foundMe = repo.GetOneStore(findMe);

                Assert.NotNull(foundMe);
                Assert.Equal(foundMe.StoreFrontID, findMe);
            }
        }

        [Fact]
        public void ViewInventoryTest()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepo repo = new Repo(context);
                int getStoreID = 1;
                List<LineItem> inventory;

                inventory = repo.ViewInventory(getStoreID);

                Assert.NotNull(inventory);
                Assert.Equal(2, inventory.Count);
            }
        }

        [Fact]
        public void GetOneItemTest()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepo repo = new Repo(context);
                int itemID = 4;

                Product foundItem = repo.GetOneItem(itemID);

                Assert.Equal(4.44, foundItem.ProductPrice);
            }
        }

        [Fact]
        public void ReplenishInventoryTest()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepo repo = new Repo(context);
                int itemID = 1;
                int amount = 1000;

                LineItem replenishedItem = repo.ReplenishInventory(itemID, amount);

                Assert.Equal(1100, replenishedItem.Quantity);
            }
        }

        [Fact]
        public void AddStoreTest()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepo repo = new Repo(context);
                StoreFront newStore = new StoreFront()
                {
                    StoreName = "3rd Tortilla Store",
                    StoreAddress = "333 Tortilla Road"
                };

                StoreFront addedMe = repo.AddStore(newStore);

                Assert.NotNull(addedMe);
                Assert.Equal(addedMe, repo.GetOneStore(newStore.StoreFrontID));
            }
        }

        [Fact]
        public void GetAllCustomersTest()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepo repo = new Repo(context);
                List<Customer> customers;

                customers = repo.GetAllCustomers();

                Assert.NotNull(customers);
                Assert.Equal(2, customers.Count);
            }
        }
        [Fact]
        public void GetOneCustomerTest()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepo repo = new Repo(context);
                string findMe = "222@customer.com";

                Customer foundMe = repo.GetOneCustomer(findMe);

                Assert.NotNull(foundMe);
                Assert.Equal(foundMe.CustomerEmail, findMe);
            }
        }

        [Fact]
        public void AddCustomerTest()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepo repo = new Repo(context);
                Customer newCustomer = new Customer()
                {
                    CustomerName = "3rd Customer",
                    CustomerAddress = "333 Customer Road",
                    CustomerEmail = "333@customer.com",
                    CustomerPhone = "0123456789"
                };

                Customer addedMe = repo.AddCustomer(newCustomer);

                Assert.Equal(addedMe, repo.GetOneCustomer(newCustomer.CustomerEmail));
            }
        }

        [Fact]
        public void GetStoreOrdersTest()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepo repo = new Repo(context);
                int getStoreID = 2;
                List<Order> storeOrders;

                storeOrders = repo.GetStoreOrders(getStoreID);

                Assert.NotNull(storeOrders);
                Assert.Equal(2, storeOrders.Count);           
            }
        }

        [Fact]
        public void GetCustomerOrdersTest()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepo repo = new Repo(context);
                string getCustomerEmail = "222@customer.com";
                Customer getCustomer;
                List<Order> customerOrders;

                getCustomer = repo.GetOneCustomer(getCustomerEmail);
                customerOrders = repo.GetCustomerOrders(getCustomer.CustomerID);

                Assert.NotNull(customerOrders);
                Assert.Equal(2, customerOrders.Count);
            }
        }

        [Fact]
        public void PlaceOrderTest()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepo repo = new Repo(context);
                Order newOrder = new Order()
                {
                    CustomerID = 1,
                    StoreFrontID = 1,
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Product = new Product()
                            {
                                ProductName = "Tortilla #2",
                                ProductPrice = 2.22,
                                ProductDescription = "2 Tortillas"
                            },
                        Quantity = 20,
                        },
                        new OrderItem
                        {
                            Product = new Product()
                            {
                                ProductName = "Tortilla #1",
                                ProductPrice = 1.11,
                                ProductDescription = "1 Tortilla"
                            },
                        Quantity = 10,
                        }
                    }
                };

                Order placeOrder = repo.PlaceOrder(newOrder);

                Assert.NotNull(placeOrder);
                Assert.Equal(2, repo.GetCustomerOrders(1).Count);
                Assert.Equal(2, repo.GetStoreOrders(1).Count);
            }
        }

        [Fact]
        public void GetProductPriceTest()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepo repo = new Repo(context);
                int productID = 1;

                double price = repo.GetProductPrice(productID);

                Assert.Equal(1.11, price);
            }
        }
    }
}
