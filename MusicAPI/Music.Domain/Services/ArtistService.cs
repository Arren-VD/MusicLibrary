using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Models;
using Music.Views;
using Music.Views.ClientViews;
using Music.Views.GlobalViews;
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

        public async Task<Artist> AddArtist(CancellationToken cancellationToken,NameDTO<string> externalArtist, int trackId)
        {
            var artist = await _repo.UpsertByCondition(x => x.ClientId == externalArtist.Id,_mapper.Map<Artist>(externalArtist));
            var trackArtist = await _repo.UpsertByCondition(x => x.ArtistId == artist.Id && x.TrackId == trackId,new TrackArtist(trackId, artist.Id));
            return artist;
        }
        public async Task<List<Artist>>AddArtistCollection(CancellationToken cancellationToken, List<NameDTO<string>> externalArtistCollection, int trackId)
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
