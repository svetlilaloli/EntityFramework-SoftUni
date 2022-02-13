namespace VaporStore
{
    using AutoMapper;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.Dto.Import;

    public class VaporStoreProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public VaporStoreProfile()
        {
            // input
            CreateMap<GameInputDto, Game>();
            CreateMap<CardInputDto, Card>()
                .ForMember(dest => dest.Cvc, opt => opt.MapFrom(s => s.CVC));
            CreateMap<UserCardsInputDto, User>();
            CreateMap<PurchaseInputDto, Purchase>();
        }
    }
}