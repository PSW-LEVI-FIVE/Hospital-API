using HospitalLibrary.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Microsoft.AspNetCore.Components.Route("api/intranet/login")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }
}