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
    public interface ITrackService
    {
        Task<Track> AddTrack(CancellationToken cancellationToken,ExternalTrackOutput externalTrack, int userId);
       void AddTrackCollection(CancellationToken cancellationToken, List<ExternalTrackOutput> tracks, int userId);
    }
}
