using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music.Domain.Contracts.Services;
using Music.Domain.Services.Helpers;
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
        public async Task<ActionResult<List<UserClientDTO>>> LinkUserToExternalAPIs(int userId,List<UserTokenDTO> spotifyTokens, CancellationToken cancellationToken)
        {
            return Ok(await (_userService.LinkUserToExternalAPIs(userId, spotifyTokens, cancellationToken)));
        }     
        [HttpPost]
        [Route("user/{userId}/import")]
        public async Task<ActionResult<List<TrackDTO>>> ImportCurrentUserTracksWithPlaylistAndArtistFromExternalServicesToDB(int userId, List<UserTokenDTO> spotifyTokens, CancellationToken cancellationToken)
        {      
            return Ok(await(_musicService.ImportClientMusicToDB(userId, spotifyTokens, cancellationToken)));
        }
        [HttpGet]
        [Route("user/{userId}/getalltracks")]
        public async Task<ActionResult<PagingWrapper<TrackDTO>>> GetUserTracks(int userId, [FromQuery] List<int> playlistIds, int page, int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await(_musicService.GetAllTracksWithPlaylistAndArtist(userId, playlistIds, page, pageSize, cancellationToken)));
        }
    }
}
