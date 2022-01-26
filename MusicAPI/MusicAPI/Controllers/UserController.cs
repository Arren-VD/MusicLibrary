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
        public ActionResult<string> CreateUser([FromBody] UserCreationDTO user)
        {
            var result = _userService.CreateUser(user);
            if (result.IsErrored())
                return BadRequest(result.Errors);
            return Ok(result.Value);
        }
        [HttpPost]
        [Route("login")]
        public ActionResult<string> Login(LoginDTO loginInfo)
        {
            return Ok(_userService.Login(loginInfo));
        }
    }
}
