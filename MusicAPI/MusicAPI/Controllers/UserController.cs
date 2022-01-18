using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music.Domain.Contracts.Services;
using Music.Views.LocalDTOs;
using Music.Views.SpotifyDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("{userid}/spotify/link/")]
        public ActionResult<string> LinkToSpotifyAPI(string spotifyToken, int userid)
        {
            return Ok(_userService.LinkUserToSpotify(spotifyToken, userid));
        }
        [HttpGet]
        [Route("spotify/me")]
        public ActionResult<string> GetSpotifyUserInfo(string spotifyToken)
        {
            return Ok(_userService.GetSpotifyUser(spotifyToken));
        }
        [HttpPost]
        [Route("create")]
        public ActionResult<string> CreateUser([FromBody] UserCreationDTO user)
        {
            return Ok(_userService.CreateUser(user));
        }
        [HttpPost]
        [Route("login")]
        public ActionResult<string> Login(LoginDTO loginInfo)
        {
            return Ok(_userService.Login(loginInfo));
        }
    }
}
