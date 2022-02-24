using Microsoft.AspNetCore.Mvc;
using Music.Domain.Contracts.Services;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }
        [HttpGet]
        [Route("user/{userId}/getallplaylists")]
        public async Task<ActionResult<List<PlaylistDTO>>> GetCurrentUserPlaylists( int userId, CancellationToken cancellationToken)
        {
            return Ok(await (_playlistService.GetAllUserPlaylists( userId, cancellationToken)));
        }
    }
}
