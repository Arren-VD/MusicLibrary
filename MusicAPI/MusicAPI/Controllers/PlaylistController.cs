using Microsoft.AspNetCore.Mvc;
using Music.Domain.Contracts.Services;
using Music.Views;
using Music.Views.Inputs;
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
        public async Task<ActionResult<List<PlaylistDTO>>> GetCurrentUserPlaylists(CancellationToken cancellationToken, int userId)
        {
            return Ok(await (_playlistService.GetAllUserPlaylists(cancellationToken, userId)));
        }
        [HttpPost]
        [Route("user/{userId}/add")]
        public async Task<ActionResult<List<TrackDTO>>> AddPlaylistToUserTrack(int userId, int trackId, PlaylistInput playlist, CancellationToken cancellationToken)
        {
            return Ok(await _playlistService.AddPlaylistToUserTrack(userId,trackId, playlist, cancellationToken));
        }
        [HttpPost]
        [Route("user/{userId}/add/client")]
        public async Task<ActionResult<List<TrackDTO>>> AddClientPlaylistTrackToPlaylistTrack(int userId, (List<UserTokenDTO> spotifyTokens, List<ClientPlaylistDTO> clientPlaylistCollection) playlistCollectionWithToken, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
