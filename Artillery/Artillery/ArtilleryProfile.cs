namespace Artillery
{
    using Artillery.Data.Models;
    using Artillery.DataProcessor.ImportDto;
    using AutoMapper;
    using System.Linq;

    class ArtilleryProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public ArtilleryProfile()
        {
            CreateMap<CountryInputDto, Country> ();
            CreateMap<ManufacturerInputDto, Manufacturer> ();
            CreateMap<ShellInputDto, Shell> ();
            CreateMap<GunInputDto, Gun>()
                .ForMember(dest => dest.CountriesGuns, opt => opt.MapFrom(x => x.Countries.Select(p => new CountryGun { CountryId = p.Id })));
        }
    }
}