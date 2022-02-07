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
            var config = new MapperConfiguration(cfg => {
                CreateMap<Playlist, PlaylistDTO>();
            });
            var mapper = config.CreateMapper();
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

            //CreateMap<Playlist, PlaylistDTO>();
            //CreateMap<PlaylistDTO, Playlist>();

            CreateMap<PlaylistTrack, PlaylistDTO>()
                 .ForMember(d => d.ClientPlayList, opt => opt.MapFrom(src => src.ClientPlaylists))
            .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Playlist.Name))
             .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Playlist.Id));
                ;
            CreateMap<PlaylistDTO, PlaylistTrack>();

            CreateMap<Playlist, PlaylistDTO>()
                            .ForMember(d => d.ClientPlayList, opt => opt.Ignore());

            CreateMap<ClientPlayListTrack, ClientPlaylistDTO>();


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
              // .ForMember(d => d.Playlists, opt => opt.MapFrom(src => mapper.Map<List<PlaylistDTO>>(src.PlaylistTracks.Select(x => x.Playlist).ToList())))
              // .ForMember(d => d.Playlists, opt => opt.MapFrom(src => src.PlaylistTracks.Select(x => x.Playlist)))
              .ForMember(d => d.Playlists, opt => opt.MapFrom(src => src.PlaylistTracks))
            .ForMember(d => d.Artists, opt => opt.Ignore())
            //  .ForMember(d => d.Artists, opt => opt.MapFrom(src => src.TrackArtists.Select(x => x.Artist)))
            //.ForMember(d=>d.Playlists, opt => opt.MapFrom(src => Map<>))
            //.ForMember(d => d.ClientTrackInfo, opt => opt.MapFrom(src => src.UserTracks.Select(x => x.ClientTracks)))
            ;
        }
    }
}
