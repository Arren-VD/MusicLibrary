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
    public class TrackService : ITrackService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Track> _trackRepository;

        public TrackService(IMapper mapper, IGenericRepository<Track> trackRepository)
        {
            _trackRepository = trackRepository;
            _mapper = mapper;
        }
        public Track AddTrack(ExternalTrackDTO track)
        {
            if (_trackRepository.FindByConditionAsync(x => x.ISRC_Id == track.ISRC_Id) == null)
                return _trackRepository.Insert(_mapper.Map<Track>(track));
            return null;
        }
        public Track GetTrackByISRC_ID(string ISRC_Id)
        {
            return _trackRepository.FindByConditionAsync(x => x.ISRC_Id == ISRC_Id);
        }
    }
}
