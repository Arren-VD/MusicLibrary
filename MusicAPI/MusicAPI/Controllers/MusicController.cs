using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music.Domain.Contracts.Services;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public ActionResult<List<UserClientDTO>> LinkUserToExternalAPIs(int userId,List<UserTokenDTO> spotifyTokens)
        {
            return Ok(_userService.LinkUserToExternalAPIs(userId,spotifyTokens));
        }     
        [HttpPost]
        [Route("user/{userId}/import")]
        public ActionResult<List<TrackDTO>> GetCurrentUserTracksWithPlaylistAndArtist(int userId, List<UserTokenDTO> spotifyTokens)
        {
            return Ok(_musicService.ImportClientMusicToDB(userId, spotifyTokens));
        }
    }
}
