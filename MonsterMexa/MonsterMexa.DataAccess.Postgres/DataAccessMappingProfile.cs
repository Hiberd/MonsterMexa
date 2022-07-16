using AutoMapper;

namespace MonsterMexa.DataAccess.Postgres
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Entities.Product, Domain.Product>().ReverseMap();
            CreateMap<Entities.Category, Domain.Category>().ReverseMap();
            CreateMap<Entities.Cart, Domain.Cart>().ReverseMap();
        }
    }
}

