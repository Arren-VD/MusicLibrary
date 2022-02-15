﻿using AutoMapper;
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
        private readonly IGenericRepository<Track> _trackRepository;
        private readonly IGenericRepository<UserTrack> _userTrackRepository;
        private readonly IGenericRepository<ClientUserTrack> _clientUserTrackRepository;
        private readonly IMapper _mapper;

        public TrackService(IGenericRepository<Track> trackRepository, IGenericRepository<UserTrack> userTrackRepository, IGenericRepository<ClientUserTrack> clientUserTrackRepository, IMapper mapper)
        {
            _trackRepository = trackRepository;
            _userTrackRepository = userTrackRepository;
            _clientUserTrackRepository = clientUserTrackRepository;
            _mapper = mapper;
        }

        public async Task<Track>  AddTrack(CancellationToken cancellationToken,ExternalTrackDTO externalTrack, int userId)
        {
            var track = await _trackRepository.FindByConditionAsync(x => x.ISRC_Id == externalTrack.ISRC_Id) ?? await _trackRepository.Insert(_mapper.Map<Track>(externalTrack));
            var userTrack = await _userTrackRepository.FindByConditionAsync(x => x.UserId == userId && x.TrackId == track.Id) ?? await _userTrackRepository.Insert(new UserTrack(track.Id, userId));
            var clientUserTrack = await _clientUserTrackRepository.FindByConditionAsync(x => x.ClientId == externalTrack.Id && x.UserTrackId == userTrack.Id) ?? await _clientUserTrackRepository.Insert(new ClientUserTrack(userTrack.Id, externalTrack.ClientServiceName, externalTrack.Id, externalTrack.Preview_url));
            return track;
        }
    }
}
