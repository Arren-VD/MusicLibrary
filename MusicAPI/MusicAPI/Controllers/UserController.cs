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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] UserCreationDTO user, CancellationToken cancellationToken)
        {
            var result = await _userService.CreateUser(user, cancellationToken);
            if (result.IsErrored())
                return BadRequest(result.Errors);
            return CreatedAtAction(nameof(GetUser), nameof(UserController), new { userId = result.Value.Id }, result.Value);
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginInfo, CancellationToken cancellationToken)
        {
            return Ok(await _userService.Login(loginInfo, cancellationToken));
        }
        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<UserDTO>> GetUser([FromRoute] int userId, CancellationToken cancellationToken)
        {
            return Ok(await _userService.GetUserById(userId, cancellationToken));
        }
    }
}
