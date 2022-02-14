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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("create")]
        public ActionResult<UserDTO> CreateUser([FromBody] UserCreationDTO user)
        {
            var result = _userService.CreateUser(user);
            if (result.IsErrored())
                return BadRequest(result.Errors);
            return CreatedAtAction(nameof(GetUser), nameof(UserController), new { userId = result.Value.Id }, result.Value);
        }
        [HttpPost]
        [Route("login")]
        public ActionResult<UserDTO> Login(LoginDTO loginInfo)
        {
            return Ok(_userService.Login(loginInfo));
        }
        [HttpGet]
        [Route("{userId}")]
        public ActionResult<UserDTO> GetUser([FromRoute] int userId)
        {
            return Ok(_userService.GetUserById(userId));
        }
    }
}
