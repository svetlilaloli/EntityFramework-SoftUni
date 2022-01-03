using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.Dto.InputDto;
using CarDealer.Dto.OutputDto;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        private static IMapper mapper;
        public static void Main(string[] args)
        {
            using CarDealerContext context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //string suppliersXmlAsString = File.ReadAllText("../../../Datasets/suppliers.xml");
            //string partsXmlAsString = File.ReadAllText("../../../Datasets/parts.xml");
            //string carsXmlAsString = File.ReadAllText("../../../Datasets/cars.xml");
            //string customersXmlAsString = File.ReadAllText("../../../Datasets/customers.xml");
            //string salesXmlAsString = File.ReadAllText("../../../Datasets/sales.xml");

            //Console.WriteLine(ImportSuppliers(context, suppliersXmlAsString));
            //Console.WriteLine(ImportParts(context, partsXmlAsString));
            //Console.WriteLine(ImportCars(context, carsXmlAsString));
            //Console.WriteLine(ImportCustomers(context, customersXmlAsString));
            //Console.WriteLine(ImportSales(context, salesXmlAsString));

            //Console.WriteLine(GetCarsWithDistance(context));
            //Console.WriteLine(GetCarsFromMakeBmw(context));
            //Console.WriteLine(GetLocalSuppliers(context));
            //Console.WriteLine(GetCarsWithTheirListOfParts(context));
            //Console.WriteLine(GetTotalSalesByCustomer(context));
            //Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Suppliers");
            var serializer = new XmlSerializer(typeof(List<SupplierInputDto>), root);
            
            var suppliers = (List<SupplierInputDto>)serializer.Deserialize(new StringReader(inputXml));

            InitializeMapper();
            
            var mappedSuppliers = mapper.Map<IEnumerable<Supplier>>(suppliers);
            
            context.Suppliers.AddRange(mappedSuppliers);
            context.SaveChanges();

            return $"Successfully imported {mappedSuppliers.Count()}";
        }
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Parts");
            var serializer = new XmlSerializer(typeof(List<PartInputDto>), root);
            
            var parts = (List<PartInputDto>)serializer.Deserialize(new StringReader(inputXml));
            
            var supplierIds = context.Suppliers.Select(s => s.Id).ToList();
            
            InitializeMapper();
            
            var mappedParts = mapper.Map<IEnumerable<Part>>(parts).Where(p => supplierIds.Contains(p.SupplierId));
            
            context.Parts.AddRange(mappedParts);
            context.SaveChanges();

            return $"Successfully imported {mappedParts.Count()}";
        }
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Cars");
            var serializer = new XmlSerializer(typeof(List<CarInputDto>), root);
            var cars = (List<CarInputDto>)serializer.Deserialize(new StringReader(inputXml));

            InitializeMapper();

            var partsIds = context.Parts.Select(p => p.Id).ToList();
            List<Car> mappedCars = new List<Car>();

            foreach (var car in cars)
            {
                car.PartIds = car.Parts.Select(p => p.Id).Distinct().ToArray();

                if (car.PartIds.All(p => partsIds.Contains(p)))
                {
                    mappedCars.Add(mapper.Map<Car>(car));
                }
            }
            context.Cars.AddRange(mappedCars);
            context.SaveChanges();

            return $"Successfully imported {mappedCars.Count}";
        }
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Customers");
            var serializer = new XmlSerializer(typeof(List<CustomerInputDto>), root);
            var customers = (List<CustomerInputDto>)serializer.Deserialize(new StringReader(inputXml));

            InitializeMapper();

            var mappedCustomers = mapper.Map<IEnumerable<Customer>>(customers);
            context.Customers.AddRange(mappedCustomers);
            context.SaveChanges();

            return $"Successfully imported {mappedCustomers.Count()}";
        }
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Sales");
            var serializer = new XmlSerializer(typeof(List<SaleInputDto>), root);
            var sales = (List<SaleInputDto>)serializer.Deserialize(new StringReader(inputXml));

            InitializeMapper();

            var carIds = context.Cars.Select(c => c.Id).ToList();
            var mappedSales = mapper.Map<IEnumerable<Sale>>(sales).Where(s => carIds.Contains(s.CarId));
            context.Sales.AddRange(mappedSales);
            context.SaveChanges();

            return $"Successfully imported {mappedSales.Count()}";
        }
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            int distance = 2000000;
            InitializeMapper();
            var cars = context.Cars.Where(c => c.TraveledDistance > distance)
                                   .OrderBy(c => c.Make)
                                   .ThenBy(c => c.Model)
                                   .Take(10)
                                   .ProjectTo<CarOutputDto>(mapper.ConfigurationProvider)
                                   .ToList();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var root = new XmlRootAttribute("cars");
            var serializer = new XmlSerializer(typeof(List<CarOutputDto>), root);
            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, cars, namespaces);
            }
            
            return sb.ToString().TrimEnd();
        }
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            InitializeMapper();
            var bmwCars = context.Cars.Where(c => c.Make == "BMW")
                                      .OrderBy(c => c.Model)
                                      .ThenByDescending(c => c.TraveledDistance)
                                      .ProjectTo<BmwCarOutputDto>(mapper.ConfigurationProvider)
                                      .ToList();
            
            var root = new XmlRootAttribute("cars");
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            var serializer = new XmlSerializer(typeof(List<BmwCarOutputDto>), root);
            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, bmwCars, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            InitializeMapper();
            var localSuppliers = context.Suppliers
                                        .Where(s => s.IsImporter == false)
                                        .ProjectTo<LocalSupplierOutputDto>(mapper.ConfigurationProvider)
                                        .ToList();

            var root = new XmlRootAttribute("suppliers");
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            var serializer = new XmlSerializer(typeof(List<LocalSupplierOutputDto>), root);
            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, localSuppliers, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            InitializeMapper();
            var carsAndParts = context.Cars.Include(c => c.PartCars)
                                           .OrderByDescending(c => c.TraveledDistance)
                                           .ThenBy(c => c.Model)
                                           .Take(5)
                                           .ProjectTo<CarWithParts>(mapper.ConfigurationProvider)
                                           .ToList();

            var root = new XmlRootAttribute("cars");
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            var serializer = new XmlSerializer(typeof(List<CarWithParts>), root);
            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, carsAndParts, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            InitializeMapper();
            var customersBoughtCar = context.Customers                                            
                                            .Where(c => c.Sales.Count > 0)
                                            .ProjectTo<CustomerBoughtCarOutputDto>(mapper.ConfigurationProvider)
                                            .OrderByDescending(c => c.SpentMoney)
                                            .ToList();

            var root = new XmlRootAttribute("customers");
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            var serializer = new XmlSerializer(typeof(List<CustomerBoughtCarOutputDto>), root);
            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, customersBoughtCar, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            InitializeMapper();
            var salesWithWithoutDiscount = context.Sales
                .ProjectTo<SalesWithDiscountOutputDto>(mapper.ConfigurationProvider)
                .ToList();

            var root = new XmlRootAttribute("sales");
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            var serializer = new XmlSerializer(typeof(List<SalesWithDiscountOutputDto>), root);
            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, salesWithWithoutDiscount, namespaces);
            }

            return sb.ToString().TrimEnd();
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
