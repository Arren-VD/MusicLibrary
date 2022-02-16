
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
    public class TrackService : ITrackService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _repo;

        public TrackService( IMapper mapper, IGenericRepository repo)
        {
            _mapper = mapper;
            _repo= repo;
        }

        public async Task<Track>  AddTrack(CancellationToken cancellationToken,ExternalTrackDTO externalTrack, int userId)
        {
            var track = await _repo.FindByConditionAsync<Track>(x => x.ISRC_Id == externalTrack.ISRC_Id);
             if ( track == null)
               track = await _repo.Insert<Track>(_mapper.Map<Track>(externalTrack));
            var userTrack = await _repo.FindByConditionAsync<UserTrack>(x => x.UserId == userId && x.TrackId == track.Id) ?? await _repo.Insert(new UserTrack(track.Id, userId));
            var clientUserTrack = await _repo.FindByConditionAsync<ClientUserTrack>(x => x.ClientId == externalTrack.Id && x.UserTrackId == userTrack.Id) ?? await _repo.Insert(new ClientUserTrack(userTrack.Id, externalTrack.ClientServiceName, externalTrack.Id, externalTrack.Preview_url));
            return track;
        }
    }
}
