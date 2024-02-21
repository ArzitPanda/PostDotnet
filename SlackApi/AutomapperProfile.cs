using AutoMapper;
using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;

namespace SlackApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<RelationRequestDto, RelationRequest>();
        }
    }

}
