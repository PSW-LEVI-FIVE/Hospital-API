using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Public
{
    [Route("api/public/login")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult UserExist(string username,string password)
        {
            User user = _userService.UserExist(username,password);
            return Ok(user);
        }
    }
}