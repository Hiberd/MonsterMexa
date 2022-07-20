using AutoMapper;

namespace MonsterMexa.API
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<Domain.Product, Contracts.GetProductResponse>();
        }
    }
}
