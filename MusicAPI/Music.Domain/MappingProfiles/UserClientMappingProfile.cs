using AutoMapper;
using Music.Models;
using Music.Views;

namespace Music.Domain.MappingProfiles
{
    public class UserClientMappingProfile : Profile
    {
        public UserClientMappingProfile()
        {
            CreateMap<UserClient, UserClientDTO>();
            CreateMap<UserClientDTO, UserClient>();
        }
    }
}
