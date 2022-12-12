using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/login")]
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
        public IActionResult GetbyId(int id)
        {
            User user = _userService.getOne(id);
            return Ok(user);
        }

        [HttpPut]
        [Route("blockuser")]
        //[Authorize(Roles = "Manager")]
        public IActionResult BlockMaliciousUser([FromQuery] int blockUserId)
        {
            User blockedUser = _userService.BlockMaliciousUser(blockUserId);
            return Ok(blockedUser);
        }

        [HttpPut]
        [Route("unblockuser")]
        //[Authorize(Roles = "Manager")]
        public IActionResult UnBlockMaliciousUser([FromQuery] int unblockUserId)
        {
            User unblockedUser = _userService.UnBlockMaliciousUser(unblockUserId);
            return Ok(unblockedUser);
        }
    }
}