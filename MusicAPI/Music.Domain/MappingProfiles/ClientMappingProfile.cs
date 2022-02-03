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
            CreateMap<Playlist, Views.ClientViews.ExternalPlaylistDTO>();
            CreateMap<Views.ClientViews.ExternalPlaylistDTO, Playlist>()
             .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<Artist, ExternalArtistDTO>();
            CreateMap<ExternalArtistDTO, Artist>()
             .ForMember(d => d.ClientId, opt => opt.MapFrom(src => src.Id))
            .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<Track, ClientTrackDTO>();
            CreateMap<ClientTrackDTO, Track>()
            .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<ExternalTrackDTO, Track>()
            .ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<Track, ExternalTrackDTO>()
            .ForMember(d => d.Playlists, opt => opt.MapFrom(src => src.PlaylistTracks.Select(x => x.Playlist)))
            .ForMember(d => d.Artists, opt => opt.MapFrom(src => src.TrackArtists.Select(x => x.Artist)));

            CreateMap<Playlist, PlaylistDTO>()
                .ForMember(d => d.ClientPlayList, opt  => opt.MapFrom(src => src.))
                ;
            CreateMap<PlaylistDTO, Playlist>();

            CreateMap<ArtistDTO, Artist>();
            CreateMap<Artist, ArtistDTO>();

            CreateMap<ClientTrackDTO, ClientUserTrack>();
            CreateMap<ClientUserTrack, ClientTrackDTO>();

            CreateMap<ClientPlaylistDTO, ClientPlayListTrack>();
            CreateMap<ClientPlayListTrack, ClientPlaylistDTO>();

            CreateMap<ClientTrackDTO, ClientUserTrack>();
            CreateMap<ClientUserTrack, ClientTrackDTO>();

            CreateMap<TrackDTO, Track>();
            CreateMap<Track, TrackDTO>()
            .ForMember(d => d.Playlists, opt => opt.MapFrom(src => src.PlaylistTracks.Select(x => x.Playlist)))
            .ForMember(d => d.Artists, opt => opt.MapFrom(src => src.TrackArtists.Select(x => x.Artist)))
            .ForMember(d => d.ClientTrackInfo, opt => opt.MapFrom(src => src.UserTracks.Select(x => x.ClientTracks)));
        }
    }
}
