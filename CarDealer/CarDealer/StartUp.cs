using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.Dto.InputDto;
using CarDealer.Dto.OutputDto;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarDealer
{
    public class StartUp
    {
        private static IMapper mapper;
        static void Main(string[] args)
        {
            using CarDealerContext context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //string suppliersJsonAsString = File.ReadAllText("../../../Datasets/suppliers.json");
            //string partsJsonAsString = File.ReadAllText("../../../Datasets/parts.json");
            //string carsJsonAsString = File.ReadAllText("../../../Datasets/cars.json");
            //string customersJsonAsString = File.ReadAllText("../../../Datasets/customers.json");
            //string salesJsonAsString = File.ReadAllText("../../../Datasets/sales.json");

            //Console.WriteLine(ImportSuppliers(context, suppliersJsonAsString));
            //Console.WriteLine(ImportParts(context, partsJsonAsString));
            //Console.WriteLine(ImportCars(context, carsJsonAsString));
            //Console.WriteLine(ImportCustomers(context, customersJsonAsString));
            //Console.WriteLine(ImportSales(context, salesJsonAsString));

            //Console.WriteLine(GetOrderedCustomers(context));
            //Console.WriteLine(GetCarsFromMakeToyota(context));
            //Console.WriteLine(GetLocalSuppliers(context));
            //Console.WriteLine(GetCarsWithTheirListOfParts(context));
            //Console.WriteLine(GetTotalSalesByCustomer(context));
            Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            IEnumerable<SupplierInputDto> suppliers = JsonConvert.DeserializeObject<IEnumerable<SupplierInputDto>>(inputJson);
            InitializeMapper();

            var mappedSuppliers = mapper.Map<IEnumerable<Supplier>>(suppliers);
            context.Suppliers.AddRange(mappedSuppliers);
            context.SaveChanges();

            return $"Successfully imported {mappedSuppliers.Count()}.";
        }
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            IEnumerable<PartInputDto> parts = JsonConvert.DeserializeObject<IEnumerable<PartInputDto>>(inputJson);
            InitializeMapper();

            List<int> existingSuppliersIds = context.Suppliers.Select(s => s.Id).ToList();
            List<Part> mappedParts = new List<Part>();

            foreach (var part in parts)
            {
                if (existingSuppliersIds.Contains(part.SupplierId))
                {
                    mappedParts.Add(mapper.Map<Part>(part));
                }
            }

            context.Parts.AddRange(mappedParts);
            context.SaveChanges();

            return $"Successfully imported {mappedParts.Count()}.";
        }
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            IEnumerable<CarInputDto> cars = JsonConvert.DeserializeObject<IEnumerable<CarInputDto>>(inputJson);
            InitializeMapper();

            var mappedCars = mapper.Map<IEnumerable<Car>>(cars);
            context.Cars.AddRange(mappedCars);
            context.SaveChanges();

            return $"Successfully imported {mappedCars.Count()}.";
        }
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            IEnumerable<CustomerInputDto> customers = JsonConvert.DeserializeObject<IEnumerable<CustomerInputDto>>(inputJson);
            InitializeMapper();

            var mappedCustomers = mapper.Map<IEnumerable<Customer>>(customers);
            context.Customers.AddRange(mappedCustomers);
            context.SaveChanges();

            return $"Successfully imported {mappedCustomers.Count()}.";
        }
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            IEnumerable<SaleInputDto> sales = JsonConvert.DeserializeObject<IEnumerable<SaleInputDto>>(inputJson);
            InitializeMapper();

            var mappedSales = mapper.Map<IEnumerable<Sale>>(sales);
            context.Sales.AddRange(mappedSales);
            context.SaveChanges();

            return $"Successfully imported {mappedSales.Count()}.";
        }
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            InitializeMapper();
            var customers = context.Customers.OrderBy(c => c.BirthDate)
                                             .ProjectTo<OrderedCustomerOutputDto>(mapper.ConfigurationProvider);

            var mappedCustomers = mapper.Map<IEnumerable<OrderedCustomerOutputDto>>(customers);
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            return JsonConvert.SerializeObject(mappedCustomers, jsonSettings);
        }
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            string make = "Toyota";

            InitializeMapper();
            var toyotaCars = context.Cars.Where(c => c.Make == make)
                                         .OrderBy(c => c.Model)
                                         .ThenByDescending(c => c.TravelledDistance)
                                         .ProjectTo<ToyotaCarOutputDto>(mapper.ConfigurationProvider);

            var mappedCars = mapper.Map<IEnumerable<ToyotaCarOutputDto>>(toyotaCars);
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(mappedCars, jsonSettings);
        }
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            InitializeMapper();
            var localSuppliers = context.Suppliers.Where(s => !s.IsImporter)
                                                  .ProjectTo<LocalSupplierOutputDto>(mapper.ConfigurationProvider);

            var mappedSuppliers = mapper.Map<IEnumerable<LocalSupplierOutputDto>>(localSuppliers);
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(mappedSuppliers, jsonSettings);
        }
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            InitializeMapper();
            var carsWithParts = context.Cars.Include(p => p.PartCars).ProjectTo<CarWithParts>(mapper.ConfigurationProvider);

            var mappedCars = mapper.Map<IEnumerable<CarWithParts>>(carsWithParts);
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(mappedCars, jsonSettings);
        }
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            InitializeMapper();
            var customersBoughtCars = context.Customers.Include(c => c.Sales)
                                                       .Where(c => c.Sales.Count > 0)
                                                       .ProjectTo<CustomerBoughtCarOutputDto>(mapper.ConfigurationProvider)
                                                       .OrderByDescending(c => c.SpentMoney)
                                                       .ThenByDescending(c => c.BoughtCars);
            
            var mappedCustomers = mapper.Map<IEnumerable<CustomerBoughtCarOutputDto>>(customersBoughtCars);
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(mappedCustomers, jsonSettings);
        }
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            InitializeMapper();
            var salesWithDiscounts = context.Sales.Include(s => s.Customer)
                                                  .Include(s => s.Car)
                                                  .ThenInclude(c => c.PartCars)
                                                  .ThenInclude(p => p.Part)
                                                  .ProjectTo<SalesWithDiscountOutputDto>(mapper.ConfigurationProvider)
                                                  .Take(10);

            var mappedSales = mapper.Map<IEnumerable<SalesWithDiscountOutputDto>>(salesWithDiscounts);
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(mappedSales, jsonSettings);
        }
        public static void InitializeMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });
            mapper = new Mapper(mapperConfiguration);
        }
    }
}
