using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music.Domain.Contracts.Services;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMusicService _musicService;
        private readonly IPlaylistService _playlistService;
        public MusicController(IUserService userService, IMusicService musicService, IPlaylistService playlistService)
        {
            _musicService = musicService;
            _userService = userService;
            _playlistService = playlistService;
        }

        [HttpPost]
        [Route("user/{userId}/link")]
        public async Task<ActionResult<List<UserClientDTO>>> LinkUserToExternalAPIs(CancellationToken cancellationToken,int userId,List<UserTokenDTO> spotifyTokens)
        {
            return Ok(await (_userService.LinkUserToExternalAPIs(cancellationToken,userId, spotifyTokens)));
        }     
        [HttpPost]
        [Route("user/{userId}/import")]
        public async Task<ActionResult<List<TrackDTO>>> ImportCurrentUserTracksWithPlaylistAndArtistFromExternalServicesToDB(CancellationToken cancellationToken,int userId, List<UserTokenDTO> spotifyTokens)
        {      
            return Ok(await(_musicService.ImportClientMusicToDB(cancellationToken,userId, spotifyTokens)));
        }
        [HttpPost]
        [Route("user/{userId}/getalltracks")]
        public async Task<ActionResult<List<TrackDTO>>> GetCurrentUserTracksWithPlaylistAndArtist(CancellationToken cancellationToken, int userId, List<UserTokenDTO> spotifyTokens, [FromQuery] List<int> playlistIds, int page, int pageSize)
        {
            return Ok(await(_musicService.GetAllTracksWithPlaylistAndArtist(cancellationToken,userId,spotifyTokens, playlistIds, page, pageSize)));
        }
        [HttpPost]
        [Route("user/{userId}/playlist/add")]
        public async Task<ActionResult<List<TrackDTO>>> AddPlaylistToUserTrack(CancellationToken cancellationToken, int userId, List<UserTokenDTO> spotifyTokens, [FromQuery] List<int> playlistIds, int page, int pageSize)
        {
            return Ok(await (_musicService.GetAllTracksWithPlaylistAndArtist(cancellationToken, userId, spotifyTokens, playlistIds, page, pageSize)));
        }
        [HttpPost]
        [Route("user/{userId}/playlist/add")]
        public async Task<ActionResult<List<TrackDTO>>> AddClientPlaylistTrackToPlaylistTrack(CancellationToken cancellationToken, int userId, List<UserTokenDTO> spotifyTokens, [FromQuery] List<int> playlistIds, int page, int pageSize)
        {
            return Ok(await (_musicService.GetAllTracksWithPlaylistAndArtist(cancellationToken, userId, spotifyTokens, playlistIds, page, pageSize)));
        }
    }
}
