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
    public interface IPlaylistTrackService
    {
        public PlaylistTrack AddPlaylistTrack(PlaylistTrack track);
        PlaylistTrack GetPlaylistTrackByJoinedIDs(int playlistId,int trackId);
    }
}
