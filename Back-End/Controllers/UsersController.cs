using Microsoft.AspNetCore.Mvc;
using Back_End.Users;
using System;
using Microsoft.AspNetCore.Http;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            if (user is null)
                return BadRequest(new ArgumentNullException());
            _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUser), new { userId = user.UserId }, user);
        }

        [HttpGet("{userId}", Name = nameof(GetUser))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetUser(string userId)
        {
            var user = _userService.GetUser(userId);
            if (user is null)
                return NotFound();
            return Ok(user);
        }
    }
}