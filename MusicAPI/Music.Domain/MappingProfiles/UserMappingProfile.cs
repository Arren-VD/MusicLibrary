using Music.Views.SpotifyDTOs;
using AutoMapper;
using Music.Models.Local;
using Music.Views.LocalDTOs;

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
