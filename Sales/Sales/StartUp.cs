using P03_SalesDatabase.Data;
using P03_SalesDatabase.Data.Models;
using System;
using System.Collections.Generic;

namespace P03_SalesDatabase
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //using SalesContext context = new SalesContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //Console.WriteLine("Sales database created!");
            //SeedDatabase(context);
        }
        private static void SeedDatabase(SalesContext context)
        {
            string[] productNames = new string[] { "Milk", "Bread", "Jam", "Coke", "Butter", "Tomatoes", "Potatoes", "Lettuce", "Onions", "Peppers", "Eggs" };
            double[] productQuantities = new double[] { 90, 102, 224, 98, 108 };
            decimal[] productPrices = new decimal[] { 1.10M, 1.25M, 1.05M, 1.85M, 1.29M, 1.5M, 0.95M, 1.58M, 2.88M };
            string[] customerNames = new string[] { "John Smith", "Светлана Василева", "svetlilaloli", "monkey555", "Lara Croft" };
            string[] customerEmails = new string[] { "john@domain.com", "svet@dom.co.uk", "vasil@somedomain.eu", "monkey@555.com", "lara@croft.com" };
            string[] storeNames = new string[] {"AliBaba", "ShopRite", "amazon", "nextLevel", "H&M"};

            List<Product> products = new List<Product>();
            List<Customer> customers = new List<Customer>();
            List<Store> stores = new List<Store>();
            List<Sale> sales = new List<Sale>();

            foreach (var name in productNames)
            {
                Product product = new Product();
                product.Name = name;
                product.Quantity = productQuantities[new Random().Next(0, productQuantities.Length - 1)];
                product.Price = productPrices[new Random().Next(0, productPrices.Length - 1)];
                
                products.Add(product);
            }

            for (int i = 0; i < customerNames.Length; i++)
            {
                Customer customer = new Customer();
                customer.Name = customerNames[i];
                customer.Email = customerEmails[i];

                customers.Add(customer);
            }

            foreach (var name in storeNames)
            {
                Store store = new Store();
                store.Name = name;

                stores.Add(store);
            }

            var date = DateTime.Now;

            for (int i = 0; i < 50; i++)
            {
                Sale sale = new Sale();
                sale.Date = date;
                sale.Product = products[new Random().Next(0, products.Count - 1)];
                sale.Customer = customers[new Random().Next(0, customers.Count - 1)];
                sale.Store = stores[new Random().Next(0, stores.Count - 1)];

                sales.Add(sale);
                date = date.AddHours(new Random().Next(2));
            }

            context.Products.AddRange(products);
            context.Customers.AddRange(customers);
            context.Stores.AddRange(stores);
            context.Sales.AddRange(sales);
            context.SaveChanges();
        }
    }
}
