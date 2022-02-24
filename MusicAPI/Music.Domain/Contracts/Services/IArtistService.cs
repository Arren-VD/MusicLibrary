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
        Task<Artist> AddArtist(ExternalArtistDTO externalArtist, int trackId, CancellationToken cancellationToken);
        Task<List<Artist>> AddArtistCollection(List<ExternalArtistDTO> externalArtistCollection, int trackId, CancellationToken cancellationToken);
    }
}
