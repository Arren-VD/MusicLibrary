using Music.Models;
using Music.Views.ClientViews;
using Music.Views.GlobalViews;
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
        Task<Artist> AddArtist(CancellationToken cancellationToken, NameDTO<string> externalArtist, int trackId);
        Task<List<Artist>> AddArtistCollection(CancellationToken cancellationToken, List<NameDTO<string>> externalArtistCollection, int trackId);
    }
}
