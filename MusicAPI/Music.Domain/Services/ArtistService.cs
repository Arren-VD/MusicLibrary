using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Models;
using Music.Views;
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
        private readonly IMapper _mapper;
        private readonly IGenericRepository _repo;

        public ArtistService(IMapper mapper, IGenericRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<TrackArtist> AddTrackArtist(ExternalArtistDTO externalArtist, int trackId, CancellationToken cancellationToken)
        {
            var artist = await _repo.UpsertByCondition(x => x.ClientId == externalArtist.Id, _mapper.Map<Artist>(externalArtist));
            var trackArtist = await _repo.UpsertByCondition(x => x.ArtistId == artist.Id && x.TrackId == trackId, new TrackArtist(trackId, artist.Id));
            trackArtist.Artist = artist;
            return trackArtist;
        }
        public async Task<List<TrackArtist>> AddTrackArtistCollection(List<ExternalArtistDTO> externalArtistCollection, int trackId, CancellationToken cancellationToken)
        {
            List<TrackArtist> artistCollection = new List<TrackArtist>();
            externalArtistCollection.ForEach(async externalArtist =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                artistCollection.Add(await AddTrackArtist( externalArtist, trackId, cancellationToken));
            });
            return artistCollection;
        }
    }
}
