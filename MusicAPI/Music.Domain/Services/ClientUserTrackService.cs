using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Models;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Services
{
    public class ClientUserTrackService : IClientUserTrackService
    {
        private readonly IGenericRepository<ClientUserTrack> _clientUserTrackRepository;

        public ClientUserTrackService(IGenericRepository<ClientUserTrack> clientUserTrackRepository)
        {
            _clientUserTrackRepository = clientUserTrackRepository;
        }
        public ClientUserTrack AddClientUserTrack(ClientUserTrack clientUserTrack)
        {
            if (_clientUserTrackRepository.FindByConditionAsync(x => x.UserTrackId == clientUserTrack.Id && x.ClientId ==clientUserTrack.ClientId) == null)
                return _clientUserTrackRepository.Insert(clientUserTrack);
            return null;
        }
        public ClientUserTrack GetClientUserTrackByIds(string clientId, int userTrackId)
        {
            return _clientUserTrackRepository.FindByConditionAsync(x => x.UserTrackId == userTrackId && x.ClientId == clientId);
        }
    }
}
