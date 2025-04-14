using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QBOTECH.DELIVERY.CORE.DTOs;
using QBOTECH.DELIVERY.CORE.Entities;
using QBOTECH.DELIVERY.CORE.Interfaces;
using QBOTECH.DELIVERY.CORE.Services;

namespace QBOTECH.DELIVERY.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;
        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UsersCreateDTO usersCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newUser = await _userService.AddUserAsync(usersCreateDTO);

                return CreatedAtAction(null,new { success = true }, newUser);
            }
            catch (Exception ex)
            {
                //Handle exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating user");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UsersUpdateDTO usersUpdateDTO)
        {
            if (id != usersUpdateDTO.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _userService.UpdateUser(usersUpdateDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                //Handle exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //Handle exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting user");
            }
        }
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] UsersSignInDTO usersSignInDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userService.SignInAsync(usersSignInDTO.Email, usersSignInDTO.Password);
                if (user == null)
                {
                    return Unauthorized();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                //Handle exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error signing in");
            }
        }

    }
}
