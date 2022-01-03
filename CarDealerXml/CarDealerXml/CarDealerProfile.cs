using AutoMapper;
using CarDealer.Dto.InputDto;
using CarDealer.Dto.OutputDto;
using CarDealer.Models;
using System.Linq;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SupplierInputDto, Supplier>();
            CreateMap<PartInputDto, Part>();
            CreateMap<CarInputDto, Car>()
                .ForMember(dest => dest.PartCars, opt => opt.MapFrom(x => x.PartIds.Distinct().Select(p => new PartCar { PartId = p })));
            CreateMap<CustomerInputDto, Customer>();
            CreateMap<SaleInputDto, Sale>();

            CreateMap<Car, CarOutputDto>();
            CreateMap<Car, BmwCarOutputDto>();
            CreateMap<Supplier, LocalSupplierOutputDto>()
                .ForMember(dest => dest.PartsCount, opt => opt.MapFrom(s => s.Parts.Count));
            CreateMap<Car, CarWithParts>()
                .ForMember(dest => dest.Parts, opt => opt.MapFrom(c => c.PartCars.Select(pc => new PartOutputDto
                {
                    Name = pc.Part.Name,
                    Price = pc.Part.Price
                }).OrderByDescending(p => p.Price)));
            CreateMap<Customer, CustomerBoughtCarOutputDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(c => c.Name))
                .ForMember(dest => dest.BoughtCars, opt => opt.MapFrom(c => c.Sales.Count))
                .ForMember(dest => dest.SpentMoney, opt => opt.MapFrom(c => c.Sales.SelectMany(s => s.Car.PartCars).Sum(p => p.Part.Price)));
            CreateMap<Sale, SalesWithDiscountOutputDto>()
                .ForMember(dest => dest.Car, opt => opt.MapFrom(c => new CarAttrOutputDto
                {
                    Make = c.Car.Make,
                    Model = c.Car.Model,
                    TraveledDistance = c.Car.TraveledDistance
                }))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(s => $"{s.Discount:f2}"))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(c => c.Customer.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(c => $"{c.Car.PartCars.Sum(p => p.Part.Price):f2}"))
                .ForMember(dest => dest.PriceWithDiscount, opt => 
                    opt.MapFrom(c => $"{c.Car.PartCars.Sum(p => p.Part.Price) - c.Car.PartCars.Sum(p => p.Part.Price)*c.Discount/100:f2}"));
        }
    }
}
