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
                .ForMember(dest => dest.PartCars, opt => opt.MapFrom(x => x.PartsId.Distinct().Select(p => new PartCar { PartId = p })));
            CreateMap<CustomerInputDto, Customer>();
            CreateMap<SaleInputDto, Sale>();

            CreateMap<Customer, OrderedCustomerOutputDto>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(c => string.Format($"{c.BirthDate:dd/MM/yyyy}")));
            CreateMap<Car, ToyotaCarOutputDto>();
            CreateMap<Supplier, LocalSupplierOutputDto>()
                .ForMember(dest => dest.PartsCount, opt => opt.MapFrom(s => s.Parts.Count));
            CreateMap<Car, CarWithParts>()
                .ForMember(dest => dest.Car, opt => opt.MapFrom(c => new CarOutputDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                }))
                .ForMember(dest => dest.Parts, opt => opt.MapFrom(c => c.PartCars.Select(p => new PartOutputDto
                {
                    Name = p.Part.Name,
                    Price = $"{p.Part.Price:f2}"
                })));
            CreateMap<Customer, CustomerBoughtCarOutputDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(c => c.Name))
                .ForMember(dest => dest.BoughtCars, opt => opt.MapFrom(c => c.Sales.Count))
                .ForMember(dest => dest.SpentMoney, opt => opt.MapFrom(c => c.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price))));
            CreateMap<Sale, SalesWithDiscountOutputDto>()
                .ForMember(dest => dest.Car, opt => opt.MapFrom(c => new CarOutputDto
                {
                    Make = c.Car.Make,
                    Model = c.Car.Model,
                    TravelledDistance = c.Car.TravelledDistance
                }))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(c => c.Customer.Name))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(s => $"{s.Discount:f2}"))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(c => $"{c.Car.PartCars.Sum(p => p.Part.Price):f2}"))
                .ForMember(dest => dest.PriceWithDiscount, opt => 
                    opt.MapFrom(c => $"{c.Car.PartCars.Sum(p => p.Part.Price) - c.Car.PartCars.Sum(p => p.Part.Price)*c.Discount/100:f2}"));
        }
    }
}
