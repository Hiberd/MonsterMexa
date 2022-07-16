using AutoMapper;

namespace MonsterMexa.API
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<Contracts.GetProductResponse, Domain.Product>().ReverseMap();
        }
    }
}
