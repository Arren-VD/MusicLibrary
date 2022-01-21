using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music.Domain.Contracts.Services;
using Music.Spotify.Views;
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
        IUserService _userService;
        public MusicController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("user/{userId}/link")]
        public ActionResult<List<UserClientDTO>> LinkUserToExternalAPIs(int userId,List<UserTokenDTO> spotifyTokens)
        {
            return Ok(_userService.LinkUserToExternalAPIs(userId,spotifyTokens));
        }
    }
}
