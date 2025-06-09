using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QBOTECH.DELIVERY.CORE.DTOs;
using QBOTECH.DELIVERY.CORE.Interfaces;

namespace QBOTECH.DELIVERY.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
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
        [HttpPost("signup")]
        [AllowAnonymous] // Allow anonymous access for sign-up
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
        [Authorize(Roles = "Admin")]
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
        [HttpPost("signin")]
        [AllowAnonymous] // Allow anonymous access for sign-in
        public async Task<IActionResult> SignIn([FromBody] UsersSignInDTO usersSignInDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _userService.SignInWithJwtAsync(usersSignInDTO.Email, usersSignInDTO.Password);
                if (response == null)
                {
                    return Unauthorized();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                //Handle exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error signing in");
            }
        }
    }
}
