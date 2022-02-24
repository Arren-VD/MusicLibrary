using Music.Models;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Music.Views.ClientViews;
using Music.Views.GlobalViews;

namespace Music.Domain.MappingProfiles
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<Artist, NameDTO<string>>();
            CreateMap<NameDTO<string>, Artist>()
             .ForMember(d => d.ClientId, opt => opt.MapFrom(src => src.Id))
            .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<ArtistDTO, Artist>();
            CreateMap<Artist, ArtistDTO>();

            CreateMap<ArtistDTO, TrackArtist>();
            CreateMap<TrackArtist, ArtistDTO>()
                  .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Artist.Id))
                  .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Artist.Name))
                  .ForMember(d => d.ClientId, opt => opt.MapFrom(src => src.Artist.ClientId));
        }
    }
}
