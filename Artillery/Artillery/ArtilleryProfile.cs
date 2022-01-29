namespace Artillery
{
    using Artillery.Data.Models;
    using Artillery.DataProcessor.ExportDto;
    using Artillery.DataProcessor.ImportDto;
    using AutoMapper;
    using System.Linq;

    class ArtilleryProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public ArtilleryProfile()
        {
            //input mappings
            CreateMap<CountryInputDto, Country>();
            CreateMap<ManufacturerInputDto, Manufacturer>();
            CreateMap<ShellInputDto, Shell>();
            CreateMap<GunInputDto, Gun>()
                .ForMember(dest => dest.CountriesGuns, opt => opt.MapFrom(x => x.Countries.Select(c => new CountryGun { CountryId = c.Id })));

            //output mappings
            CreateMap<Gun, GunOutputDto>();
            CreateMap<Shell, ShellOutputDto>();
            CreateMap<Country, CountryOutputDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.CountryName));
            CreateMap<Gun, ManufacturerGunsOutputDto>()
                .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.Manufacturer.ManufacturerName))
                .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.CountriesGuns));
        }
    }
}