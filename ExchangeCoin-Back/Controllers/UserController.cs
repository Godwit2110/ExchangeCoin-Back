using ExchangeCoin_Back.Data.Entities;
using ExchangeCoin_Back.Data.Models_DTOs;
using ExchangeCoin_Back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExchangeCoin_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private UserService _userServices;

        public UserController(UserService userService)
        {
            _userServices = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            Console.WriteLine("todoOK");
            return Ok(_userServices.GetUsers());

        }

        [HttpPost]
        public IActionResult GetUserbyID(int id)
        {
            return Ok(_userServices.GetUser(id));

        }
        [HttpGet("Get-Subscription")]
        public IActionResult GetSubscription()
        {
            Subscription respond = new Subscription();
            int UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            respond = _userServices.GetSubscription(UserID);

            return Ok(respond);

        }
        [HttpGet("Get-Logged-User")]
        public IActionResult GetLoggedUser()
        {
            LoggedUserDTO respond = new LoggedUserDTO();
            int UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            respond = _userServices.GetLoggedUser(UserID);

            return Ok(respond);

        }
        [HttpPut("Change-Subscription")]
        public IActionResult ChangeSubscription([FromQuery] int idSubs)
        {
            int UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            User user = _userServices.ChangeSubscription(UserID, idSubs);


            return Ok(user);

        }
        [HttpGet("Get-User-For-Admin")]
        public IActionResult GetUserForAdmin()
        {

            return Ok(_userServices.GetUsers());
        }

        [AllowAnonymous]
        [HttpPost("Create-User")]
        public IActionResult CreateUser(CreateandUpdateUserDTO dto)
        {
            try
            {
                _userServices.Create(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Created("Created", dto);
        }
    }
}
