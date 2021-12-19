using AutoMapper;
using ProductShop.Datasets.Dtos.Input;
using ProductShop.Datasets.Dtos.Output;
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
                .ForMember(dest => dest.Seller, opt => opt.MapFrom(p => string.Concat(p.Seller.FirstName, " ", p.Seller.LastName)));

            CreateMap<User, UsersSoldOutputDto>()
                .ForMember(dest => dest.SoldProducts, opt => opt.MapFrom(u => u.ProductsSold
                                                                               .Where(p => p.Buyer != null)
                                                                               .Select(p => new SoldProductsOutputDto()
                                                                               {
                                                                                   Name = p.Name,
                                                                                   Price = p.Price,
                                                                                   BuyerFirstName = p.Buyer.FirstName,
                                                                                   BuyerLastName = p.Buyer.LastName
                                                                               })));

            CreateMap<Category, CategoriesByProductCountOutputDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(c => c.Name))
                .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(c => c.CategoryProducts.Count))
                .ForMember(dest => dest.AveragePrice, opt => opt.MapFrom(c => $"{(c.CategoryProducts.Count == 0 ? 0 : c.CategoryProducts.Sum(p => p.Product.Price) / c.CategoryProducts.Count):f2}"))
                .ForMember(dest => dest.TotalRevenue, opt => opt.MapFrom(c => $"{c.CategoryProducts.Sum(p => p.Product.Price):f2}"));

            CreateMap<User, UserWithProductsOutputDto>()
                .ForMember(dest => dest.SoldProducts, opt => opt.MapFrom(u => new ProductsCountOutputDto()
                                                                        {
                                                                            Count = u.ProductsSold.Count(p => p.BuyerId != null),
                                                                            Products = u.ProductsSold
                                                                                        .Where(p => p.BuyerId != null)
                                                                                        .Select(p => new ProductOutputDto()
                                                                                        {
                                                                                            Name = p.Name,
                                                                                            Price = p.Price
                                                                                        })
                                                                        }));
        }
    }
}
