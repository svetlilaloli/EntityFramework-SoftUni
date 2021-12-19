using ProductShop.Data;
using ProductShop.Models;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using ProductShop.Datasets.Dtos.Input;
using AutoMapper;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Serialization;
using ProductShop.Datasets.Dtos.Output;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ProductShop
{
    public class StartUp
    {
        private static IMapper mapper;

        static void Main(string[] args)
        {
            using var context = new ProductShopContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //string usersJsonAsString = File.ReadAllText("../../../Datasets/users.json");
            //string productsJsonAsString = File.ReadAllText("../../../Datasets/products.json");
            //string categoriesJsonAsString = File.ReadAllText("../../../Datasets/categories.json");
            //string categoriesProductsJsonAsString = File.ReadAllText("../../../Datasets/categories-products.json");

            //Console.WriteLine(ImportUsers(context, usersJsonAsString));
            //Console.WriteLine(ImportProducts(context, productsJsonAsString));
            //Console.WriteLine(ImportCategories(context, categoriesJsonAsString));
            //Console.WriteLine(ImportCategoryProducts(context, categoriesProductsJsonAsString));
            //Console.WriteLine(GetProductsInRange(context));
            //Console.WriteLine(GetSoldProducts(context));
            //Console.WriteLine(GetCategoriesByProductsCount(context));
            Console.WriteLine(GetUsersWithProducts(context));
        }
        public static void InitializeMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            mapper = new Mapper(mapperConfiguration);
        }
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            IEnumerable<UserInputDto> users = JsonConvert.DeserializeObject<IEnumerable<UserInputDto>>(inputJson);
            // using AUTOMAPPER
            InitializeMapper();

            var mappedUsers = mapper.Map<IEnumerable<User>>(users);

            // using STATIC MAPPING
            //IEnumerable<User> mappedUsers = users.Select(x => x.MapToDomainUser()).ToList();

            context.Users.AddRange(mappedUsers);
            context.SaveChanges();

            return $"Successfully imported {mappedUsers.Count()}";
        }
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            IEnumerable<ProductInputDto> products = JsonConvert.DeserializeObject<IEnumerable<ProductInputDto>>(inputJson);
            InitializeMapper();

            var mappedProducts = mapper.Map<IEnumerable<Product>>(products);

            context.Products.AddRange(mappedProducts);
            context.SaveChanges();

            return $"Successfully imported {mappedProducts.Count()}";
        }
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            IEnumerable<CategoryInputDto> categories = JsonConvert.DeserializeObject<IEnumerable<CategoryInputDto>>(inputJson)
                .Where(x => !string.IsNullOrEmpty(x.Name));
            InitializeMapper();

            var mappedCategories = mapper.Map<IEnumerable<Category>>(categories);

            context.Categories.AddRange(mappedCategories);
            context.SaveChanges();

            return $"Successfully imported {mappedCategories.Count()}";
        }
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            IEnumerable<CategoriesProductsInputDto> categoriesProducts = JsonConvert.DeserializeObject<IEnumerable<CategoriesProductsInputDto>>(inputJson);
            InitializeMapper();

            var mappedCategoriesProducts = mapper.Map<IEnumerable<CategoryProduct>>(categoriesProducts);
            context.CategoryProducts.AddRange(mappedCategoriesProducts);
            context.SaveChanges();

            return $"Successfully imported {categoriesProducts.Count()}";
        }
        public static string GetProductsInRange(ProductShopContext context)
        {
            int start = 500;
            int end = 1000;

            InitializeMapper();

            var productsInRange = context.Products.Where(p => p.Price >= start && p.Price <= end)
                                                  .OrderBy(p => p.Price)
                                                  .ProjectTo<ProductsInRangeOutputDto>(mapper.ConfigurationProvider);

            var mappedProducts = mapper.Map<IEnumerable<ProductsInRangeOutputDto>>(productsInRange);

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
            };
            return JsonConvert.SerializeObject(mappedProducts, jsonSettings);
        }
        public static string GetSoldProducts(ProductShopContext context)
        {
            InitializeMapper();

            var sellers = context.Users
                .Include(p => p.ProductsSold)
                .Where(u => u.ProductsSold.Count(p => p.Buyer != null) > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ProjectTo<UsersSoldOutputDto>(mapper.ConfigurationProvider);      

            var mappedProducts = mapper.Map<IEnumerable<UsersSoldOutputDto>>(sellers);
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
            };
            return JsonConvert.SerializeObject(mappedProducts, jsonSettings);
        }
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            InitializeMapper();

            var categories = context.Categories
                                    .OrderByDescending(c => c.CategoryProducts.Count)
                                    .ProjectTo<CategoriesByProductCountOutputDto>(mapper.ConfigurationProvider);
            
            var mappedCategories = mapper.Map<IEnumerable<CategoriesByProductCountOutputDto>>(categories);

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
            };
            return JsonConvert.SerializeObject(mappedCategories, jsonSettings);
        }
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            InitializeMapper();

            var users = context.Users
                               .Include(u => u.ProductsSold)
                               .Where(u => u.ProductsSold.Count(p => p.BuyerId != null) > 0)
                               .OrderByDescending(u => u.ProductsSold.Count(p => p.BuyerId != null))
                               .ToList();

            List<UserWithProductsOutputDto> userWithProducts = new List<UserWithProductsOutputDto>();

            foreach (var user in users)
            {
                userWithProducts.Add(mapper.Map<UserWithProductsOutputDto>(user));
            }

            AllUsersSoldOutputDto allUsersSold = new AllUsersSoldOutputDto
            {
                UsersCount = userWithProducts.Count,
                Users = userWithProducts
            };


            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
            };
            return JsonConvert.SerializeObject(allUsersSold, jsonSettings);
        }
    }
}
