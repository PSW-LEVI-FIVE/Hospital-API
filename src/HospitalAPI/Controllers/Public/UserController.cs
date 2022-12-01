using System.Threading.Tasks;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Public
{
    [Route("api/public/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IRegistrationValidationService _validationService;

        public UserController(IUserService userService, IRegistrationValidationService validationService)
        {
            _userService = userService;
            _validationService = validationService;
        }
        
        [HttpGet]
        [Route("validate/username/{username}")]
        public async Task<IActionResult> IsUsernameUnique(string username)
        {
            bool isUsernameUnique = await _validationService.IsUsernameUnique(username);
            return Ok(isUsernameUnique);
        }
        
        [HttpGet]
        [Route("validate/email/{email}")]
        public async Task<IActionResult> IsEmailUnique(string email)
        {
            bool isEmailUnique = await _validationService.IsEmailUnique(email);
            return Ok(isEmailUnique);
        }
        
        [HttpGet]
        [Route("validate/uid/{uid}")]
        public async Task<IActionResult> IsUidUnique(string uid)
        {
            bool isUidUnique = await _validationService.IsUidUnique(uid);
            return Ok(isUidUnique);
        }
    }
}