using KitaraKauppa.Service.UsersService;
using KitaraKauppa.Service.UsersService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Presentation.Controller
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IUserManagement _userManagement;

        public UserController(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers(
            [FromQuery] UsersQueryOptions query)
        {

            if (query.PageNo == 0) throw new ArgumentException("Invalid pageNo parameter");

            if (query.PageSize == 0) throw new ArgumentException("Invalid pageSize parameter");

            var users = await _userManagement.GetUsers(query);

            return Ok(users);
        }

        [Authorize(Policy = "AdminOrUserIdPolicy")]
        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid userId)
        {
            var user = await _userManagement.GetUser(userId);
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("profileStatus")]
        public async Task<IActionResult> ActiveInactiveUser([FromQuery] Guid userId, [FromQuery] bool status)
        {
            await _userManagement.ActiveInactiveUser(userId, status);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUpdateUserDto user)
        {
            //Validate user model
            if (!ModelState.IsValid) throw new ArgumentException(String.Join(" | ", ModelState.Values.SelectMany(e => e.Errors)));

            var createdUser = await _userManagement.CreateUser(user);
            return Created(nameof(GetUser), createdUser);
        }

        [HttpPost("profile")]
        public async Task<IActionResult> CreateProfile(CreateDetailedUserDto profile)
        {
            var createdProfile = await _userManagement.CreateProfile(profile);
            createdProfile.Password = String.Empty;
            return Created(nameof(GetUser), createdProfile);
        }

        [Authorize(Policy = "UserIdPolicy")]
        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] CreateUpdateUserDto user)
        {
            await _userManagement.UpdateUser(userId, user);
            return NoContent();
        }
    }
}
