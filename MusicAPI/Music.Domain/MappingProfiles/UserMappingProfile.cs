using AutoMapper;
using Music.Models;
using Music.Views;

namespace Music.Domain.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();

            CreateMap<UserCreationDTO, User>();
            CreateMap<User, UserCreationDTO>();

        }
    }
}
