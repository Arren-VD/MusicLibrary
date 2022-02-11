using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Models;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IGenericRepository<Artist> _artistRepository;
        private readonly IGenericRepository<TrackArtist> _trackArtistRepository;
        private readonly IMapper _mapper;

        public ArtistService(IGenericRepository<Artist> artistRepository, IGenericRepository<TrackArtist> trackArtistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _trackArtistRepository = trackArtistRepository;
            _mapper = mapper;
        }

        public Artist AddArtist(ExternalArtistDTO externalArtist, int trackId)
        {
            var artist = _artistRepository.FindByConditionAsync(x => x.ClientId == externalArtist.Id) ?? _artistRepository.Insert(_mapper.Map<Artist>(externalArtist));
            var trackArtist = _trackArtistRepository.FindByConditionAsync(x => x.ArtistId == artist.Id && x.TrackId == trackId) ?? _trackArtistRepository.Insert(new TrackArtist(trackId, artist.Id));
            return artist;
        }
    }
}
