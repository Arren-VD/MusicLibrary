using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Models;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music.Domain.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IGenericRepository<Artist> _artistRepository;
        private readonly IGenericRepository<TrackArtist> _trackArtistRepository;
        private readonly IMapper _mapper;
        private readonly ITrueGenericRepository _repo;

        public ArtistService(IGenericRepository<Artist> artistRepository, IGenericRepository<TrackArtist> trackArtistRepository, IMapper mapper, ITrueGenericRepository repo)
        {
            _artistRepository = artistRepository;
            _trackArtistRepository = trackArtistRepository;
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<Artist> AddArtist(CancellationToken cancellationToken,ExternalArtistDTO externalArtist, int trackId)
        {
            var artist = await _repo.FindByConditionAsync<Artist>(x => x.ClientId == externalArtist.Id) ?? await _repo.Insert<Artist>(_mapper.Map<Artist>(externalArtist));
            var trackArtist = await _repo.FindByConditionAsync<TrackArtist>(x => x.ArtistId == artist.Id && x.TrackId == trackId) ?? await _repo.Insert<TrackArtist>(new TrackArtist(trackId, artist.Id));
            return artist;
        }
        public async Task<List<Artist>>AddArtistCollection(CancellationToken cancellationToken, List<ExternalArtistDTO> externalArtistCollection, int trackId)
        {
            List<Artist> artistCollection = new List<Artist>();
            externalArtistCollection.ForEach(async externalArtist =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                artistCollection.Add(await AddArtist(cancellationToken, externalArtist, trackId));
            });
            return artistCollection;
        }
    }
}
