using Music.Models;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Music.Views.ClientViews;

namespace Music.Domain.MappingProfiles
{
    public class PlaylistProfile : Profile
    {
        public PlaylistProfile()
        {
            CreateMap<Playlist, ExternalPlaylistDTO>();
            CreateMap<ExternalPlaylistDTO, Playlist>()
             .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<PlaylistTrack, PlaylistDTO>()
                 .ForMember(d => d.ClientPlayList, opt => opt.MapFrom(src => src.ClientPlaylists))
            .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Playlist.Name))
             .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Playlist.Id));
                ;
            CreateMap<PlaylistDTO, PlaylistTrack>();

            CreateMap<Playlist, PlaylistDTO>()
                            .ForMember(d => d.ClientPlayList, opt => opt.Ignore());

            CreateMap<ClientPlayListTrack, ClientPlaylistDTO>();

            CreateMap<ClientPlaylistDTO, ClientPlayListTrack>();
            CreateMap<ClientPlayListTrack, ClientPlaylistDTO>();       
        }
    }
}
