using AutoMapper;
using ProductShop.Dto.Input;
using ProductShop.Dto.Output;
using ProductShop.Models;
using System.Linq;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<UserInputDto, User>();
            CreateMap<ProductInputDto, Product>();
            CreateMap<CategoryInputDto, Category>();
            CreateMap<CategoriesProductsInputDto, CategoryProduct>();

            CreateMap<Product, ProductsInRangeOutputDto>()
                .ForMember(dest => dest.Buyer, opt => opt.MapFrom(p => p.Buyer.FirstName + ' ' + p.Buyer.LastName));

            CreateMap<User, UsersSoldOutputDto>()
                .ForMember(dest => dest.SoldProducts, opt => opt.MapFrom(u => u.ProductsSold
                                                                               .Select(p => new ProductOutputDto()
                                                                               {
                                                                                   Name = p.Name,
                                                                                   Price = p.Price
                                                                               })));

            CreateMap<Category, CategoriesByProductCountOutputDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(c => c.CategoryProducts.Count))
                .ForMember(dest => dest.AveragePrice, opt => opt.MapFrom(c => c.CategoryProducts.Count == 0 ? 0m : c.CategoryProducts.Average(p => p.Product.Price)))
                .ForMember(dest => dest.TotalRevenue, opt => opt.MapFrom(c => c.CategoryProducts.Sum(p => p.Product.Price)));

            CreateMap<User, UserWithProductsOutputDto>()
                .ForMember(dest => dest.SoldProducts, opt => opt.MapFrom(u => new ProductsCountOutputDto()
                {
                    Count = u.ProductsSold.Count,
                    Products = u.ProductsSold
                        .OrderByDescending(p => p.Price)
                        .Select(p => new ProductOutputDto()
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .ToArray()
                }));
        }
    }
}
