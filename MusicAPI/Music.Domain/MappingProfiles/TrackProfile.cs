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
    public class TrackProfile : Profile
    {
        public TrackProfile()
        {
            CreateMap<Track, ClientTrackDTO>();
            CreateMap<ClientTrackDTO, Track>()
            .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<ExternalTrackOutput, Track>()
            .ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<Track, ExternalTrackOutput>()
            .ForMember(d => d.Playlists, opt => opt.MapFrom(src => src.PlaylistTracks.Select(x => x.Playlist)))
            .ForMember(d => d.Artists, opt => opt.MapFrom(src => src.TrackArtists.Select(x => x.Artist)));


            CreateMap<ClientTrackDTO, ClientUserTrack>();
            CreateMap<ClientUserTrack, ClientTrackDTO>();

            CreateMap<ClientTrackDTO, ClientUserTrack>();
            CreateMap<ClientUserTrack, ClientTrackDTO>();

            CreateMap<TrackDTO, Track>();
            CreateMap<Track, TrackDTO>()
              .ForMember(d => d.Playlists, opt => opt.MapFrom(src => src.PlaylistTracks))
            .ForMember(d => d.Artists, opt => opt.MapFrom(src => src.TrackArtists))
            ;
        }
    }
}
