using Music.Models;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Music.Domain.MappingProfiles
{
    public class SpotifyMappingProfile : Profile
    {
        public SpotifyMappingProfile()
        {
            /*CreateMap<SpotifyUser, SpotifyUserDTO>();
            CreateMap<SpotifyUserDTO, SpotifyUser>();

            CreateMap<SpotifyUser, User>()
                  .ForMember(u => u.SpotifyId, act => act.MapFrom(src => src.Id));
            CreateMap<User, SpotifyUser>()
                 .ForMember(u => u.Id, act => act.MapFrom(src => src.SpotifyId));
            ;*/
        }
    }
}
