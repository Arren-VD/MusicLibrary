using Music.Models;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Services
{
    public interface IArtistService
    {
        Task<TrackArtist> AddTrackArtist(ExternalArtistDTO externalArtist, int trackId, CancellationToken cancellationToken);
        Task<List<TrackArtist>> AddTrackArtistCollection(List<ExternalArtistDTO> externalArtistCollection, int trackId, CancellationToken cancellationToken);
    }
}
