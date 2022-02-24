using AutoMapper;
using Music.Models;
using Music.Spotify.Models;
using Music.Spotify.Models.PlaylistModels;
using Music.Views;
using Music.Views.ClientViews;
using Music.Views.GlobalViews;
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
            CreateMap<ExternalUserDTO, SpotifyUser>();
            CreateMap<SpotifyUser, ExternalUserDTO>();

            CreateMap<ExternalTrackOutput, SpotifyTrack>();
            CreateMap<SpotifyTrack, ExternalTrackOutput>()
                .ForMember(d => d.ISRC_Id, opt => opt.MapFrom(src => src.external_ids.isrc));

            CreateMap<SpotifyArtist, NameDTO<string>>();
            CreateMap<NameDTO<string>, SpotifyArtist>();
        }
    }
}
