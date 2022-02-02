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
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<Playlist, ClientPlaylistDTO>();
            CreateMap<ClientPlaylistDTO, Playlist>()
             .ForMember(d => d.ClientId, opt => opt.MapFrom(src => src.Id))
             .ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<Artist, ClientArtistDTO>();
            CreateMap<ClientArtistDTO, Artist>()
             .ForMember(d => d.ClientId, opt => opt.MapFrom(src => src.Id))
            .ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<Track, ClientTrackDTO>();
            CreateMap<ClientTrackDTO, Track>()
             .ForMember(d => d.ClientId, opt => opt.MapFrom(src => src.Id))
            .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<Track, TrackDTO>()
            .ForMember(d => d.Playlists, opt => opt.MapFrom(src => src.PlaylistTracks.Select(x => x.Playlist)))
            .ForMember(d => d.Artists, opt => opt.MapFrom(src => src.TrackArtists.Select(x => x.Artist)));
                           // .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<TrackDTO, Track>();

            CreateMap<Playlist, PlaylistDTO>();
            CreateMap<PlaylistDTO, Playlist>();

            CreateMap<ArtistDTO, Artist>();
            CreateMap<Artist, ArtistDTO>();
        }
    }
}
