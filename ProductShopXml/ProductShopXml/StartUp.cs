using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using ProductShop.Dto.Input;
using ProductShop.Dto.Output;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        private static IMapper mapper;
        public static void Main(string[] args)
        {
            using ProductShopContext context = new ProductShopContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //string usersXmlAsString = File.ReadAllText("../../../Datasets/users.xml");
            //string productsXmlAsString = File.ReadAllText("../../../Datasets/products.xml");
            //string categoriesXmlAsString = File.ReadAllText("../../../Datasets/categories.xml");
            //string categoriesProductsXmlAsString = File.ReadAllText("../../../Datasets/categories-products.xml");

            //Console.WriteLine(ImportUsers(context, usersXmlAsString));
            //Console.WriteLine(ImportProducts(context, productsXmlAsString));
            //Console.WriteLine(ImportCategories(context, categoriesXmlAsString));
            //Console.WriteLine(ImportCategoryProducts(context, categoriesProductsXmlAsString));
            //Console.WriteLine(GetProductsInRange(context));
            //Console.WriteLine(GetSoldProducts(context));
            //Console.WriteLine(GetCategoriesByProductsCount(context));
            Console.WriteLine(GetUsersWithProducts(context));
        }
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Users");
            var serializer = new XmlSerializer(typeof(List<UserInputDto>), root);
            var users = (List<UserInputDto>)serializer.Deserialize(new StringReader(inputXml));

            InitializeMapper();

            var mappedUsers = mapper.Map<IEnumerable<User>>(users);
            context.Users.AddRange(mappedUsers);
            context.SaveChanges();

            return $"Successfully imported {mappedUsers.Count()}";
        }
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Products");
            var serializer = new XmlSerializer(typeof(List<ProductInputDto>), root);
            var products = (List<ProductInputDto>)serializer.Deserialize(new StringReader(inputXml));

            InitializeMapper();

            var mappedProducts = mapper.Map<IEnumerable<Product>>(products);
            context.Products.AddRange(mappedProducts);
            context.SaveChanges();

            return $"Successfully imported {mappedProducts.Count()}";
        }
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Categories");
            var serializer = new XmlSerializer(typeof(List<CategoryInputDto>), root);
            var categories = (List<CategoryInputDto>)serializer.Deserialize(new StringReader(inputXml));

            InitializeMapper();

            var mappedCategories = mapper.Map<IEnumerable<Category>>(categories).Where(x => !string.IsNullOrEmpty(x.Name));
            context.Categories.AddRange(mappedCategories);
            context.SaveChanges();

            return $"Successfully imported {mappedCategories.Count()}";
        }
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var root = new XmlRootAttribute("CategoryProducts");
            var serializer = new XmlSerializer(typeof(List<CategoriesProductsInputDto>), root);
            var categoryProducts = (List<CategoriesProductsInputDto>)serializer.Deserialize(new StringReader(inputXml));

            InitializeMapper();

            var mappedCategoryProducts = mapper.Map<IEnumerable<CategoryProduct>>(categoryProducts)
                .Where(cp => cp.CategoryId != 0 && cp.ProductId != 0);
            context.CategoryProducts.AddRange(mappedCategoryProducts);
            context.SaveChanges();

            return $"Successfully imported {mappedCategoryProducts.Count()}";
        }
        public static string GetProductsInRange(ProductShopContext context)
        {
            InitializeMapper();
            int start = 500;
            int end = 1000;
            var productsInRange = context.Products
                                         .Where(p => p.Price >= start && p.Price <= end)
                                         .OrderBy(p => p.Price)
                                         .Take(10)
                                         .ProjectTo<ProductsInRangeOutputDto>(mapper.ConfigurationProvider)
                                         .ToList();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var root = new XmlRootAttribute("Products");
            var serializer = new XmlSerializer(typeof(List<ProductsInRangeOutputDto>), root);

            StringBuilder sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, productsInRange, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetSoldProducts(ProductShopContext context)
        {
            InitializeMapper();
            var usersSold = context.Users.Where(u => u.ProductsSold.Count(p => p.Buyer != null) > 0)
                                         .OrderBy(u => u.LastName)
                                         .ThenBy(u => u.FirstName)
                                         .Take(5)
                                         .ProjectTo<UsersSoldOutputDto>(mapper.ConfigurationProvider)
                                         .ToList();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var root = new XmlRootAttribute("Users");
            var serializer = new XmlSerializer(typeof(List<UsersSoldOutputDto>), root);
            StringBuilder sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, usersSold, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            InitializeMapper();
            var categories = context.Categories.ProjectTo<CategoriesByProductCountOutputDto>(mapper.ConfigurationProvider)
                                               .OrderByDescending(c => c.Count)
                                               .ThenBy(c => c.TotalRevenue)
                                               .ToList();

            var root = new XmlRootAttribute("Categories");
            var serializer = new XmlSerializer(typeof(List<CategoriesByProductCountOutputDto>), root);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, categories, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            InitializeMapper();
            var users = context.Users.Include(u => u.ProductsSold)
                                     .Where(u => u.ProductsSold.Count > 0)
                                     .ProjectTo<UserWithProductsOutputDto>(mapper.ConfigurationProvider)
                                     .OrderByDescending(u => u.SoldProducts.Count)
                                     .ToArray();

            AllUsersSoldOutputDto allUsersSold = new AllUsersSoldOutputDto()
            {
                UsersCount = users.Length,
                Users = users
            };

            var root = new XmlRootAttribute("Users");
            var serializer = new XmlSerializer(typeof(AllUsersSoldOutputDto), root);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();
            

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, allUsersSold, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
        public static void InitializeMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            mapper = new Mapper(mapperConfiguration);
        }
    }
}
