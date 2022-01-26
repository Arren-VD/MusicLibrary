using AutoMapper;
using Music.Models;
using Music.Spotify.Models;
using Music.Spotify.Views;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Domain.MappingProfiles
{
    public class SpotifyMappingProfile : Profile
    {
        public SpotifyMappingProfile()
        {
            CreateMap<ExternalUserDTO, ClientUser>();
            CreateMap<ClientUser, ExternalUserDTO>();

            CreateMap<ClientTrackDTO, ClientTrack>();
            CreateMap<ClientTrack, ClientTrackDTO>();

            CreateMap<ClientTrackCollectionDTO, ClientTrackCollection>();
            CreateMap<ClientTrackCollection, ClientTrackCollectionDTO>();

            CreateMap<ClientArtistDTO, ClientArtist>();
            CreateMap<ClientArtist, ClientArtistDTO>();

            CreateMap<ExternalUrlsDTO, ExternalUrls>();
            CreateMap<ExternalUrls, ExternalUrlsDTO>();

        }
    }
}
