
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
            var track = await  _repo.UpsertByCondition(x => x.ISRC_Id == externalTrack.ISRC_Id,_mapper.Map<Track>(externalTrack));
            var userTrack = await _repo.UpsertByCondition(x => x.UserId == userId && x.TrackId == track.Id,new UserTrack(track.Id, userId));
            var clientUserTrack = await _repo.UpsertByCondition(x => x.ClientId == externalTrack.Id && x.UserTrackId == userTrack.Id,new ClientUserTrack(userTrack.Id, externalTrack.ClientServiceName, externalTrack.Id, externalTrack.Preview_url));
            return track;
        }
        public void AddTrackCollection(CancellationToken cancellationToken, List<ExternalTrackDTO> tracks,int userId)
        {
            var mapped = _mapper.Map<List<Track>>(tracks);
            _repo.UpsertRangeByCondition<Track>(mapped);
        }
    }
}
