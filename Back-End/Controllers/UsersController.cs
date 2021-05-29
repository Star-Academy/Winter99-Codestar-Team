using Microsoft.AspNetCore.Mvc;
using Back_End.Users;
using System;
using Microsoft.AspNetCore.Http;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult AddUser([FromBody] User user)
        {
            if (user?.UserId is null || user.Email is null)
                return BadRequest(new ArgumentNullException(nameof(user)));
            if (_userService.Exists(nameof(user.UserId), user.UserId))
                return Conflict($"A user with this Id already exists.");
            if (_userService.Exists(nameof(user.Email), user.Email))
                return Conflict($"A user with this Email already exists.");
            _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUser), new {userId = user.UserId}, user);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<Session> Login([FromBody] User user)
        {
            if (user?.UserId is null || user.Password is null || !_userService.CheckUser(user.UserId, user.Password))
                return Unauthorized("This UserId and Password combination does not exist");
            var session = _userService.CreateSession();
            HttpContext.Session.SetString(session.SessionId, user.UserId);
            return Ok(session);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult ValidateSession([FromBody] Session session)
        {
            var userId = HttpContext.Session.GetString(session.SessionId);
            if (userId is null)
                return NotFound();
            return Ok(userId);
        }
    }
}