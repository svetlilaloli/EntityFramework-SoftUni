namespace SoftJail
{
    using AutoMapper;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ImportDto;
    using System.Linq;

    public class SoftJailProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public SoftJailProfile()
        {
            // import
            CreateMap<CellInputDto, Cell>();
            CreateMap<DepartmentCellInputDto, Department>();
            CreateMap<MailInputDto, Mail>();
            CreateMap<PrisonerMailInputDto, Prisoner>();
            CreateMap<PrisonerInputDto, Prisoner>();
            CreateMap<OfficerPrisonerInputDto, Officer>()
                .ForMember(dest => dest.OfficerPrisoners, opt => opt.MapFrom(op => op.Prisoners
                                                                    .Select(p => new OfficerPrisoner()
                                                                    {
                                                                        PrisonerId = p.Id
                                                                    })
                                                                    .ToList()));
        }
    }
}
