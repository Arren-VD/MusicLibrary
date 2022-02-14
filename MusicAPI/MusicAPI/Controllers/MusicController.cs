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
        public MusicController(IUserService userService, IMusicService musicService)
        {
            _musicService = musicService;
            _userService = userService;
        }

        [HttpPost]
        [Route("user/{userId}/link")]
        public async Task<ActionResult<List<UserClientDTO>>> LinkUserToExternalAPIs(CancellationToken cancellationToken,int userId,List<UserTokenDTO> spotifyTokens)
        {
            return Ok(await (_userService.LinkUserToExternalAPIs(cancellationToken,userId, spotifyTokens)));
        }     
        [HttpPost]
        [Route("user/{userId}/import")]
        public async Task<ActionResult<List<TrackDTO>>> GetCurrentUserTracksWithPlaylistAndArtist(CancellationToken cancellationToken,int userId, List<UserTokenDTO> spotifyTokens)
        {      
            return Ok(await(_musicService.ImportClientMusicToDB(cancellationToken,userId, spotifyTokens)));
        }
    }
}
