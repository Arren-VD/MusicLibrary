using AutoMapper;
using Music.Models;
using Music.Views;

namespace Music.Domain.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();

            CreateMap<UserCreationDTO, User>();
            CreateMap<User, UserCreationDTO>();

            CreateMap<UserClient, UserClientDTO>();
            CreateMap<UserClientDTO, UserClient>();
        }
    }
}
