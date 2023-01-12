using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.Invitations;
using HospitalLibrary.Invitations.Dtos;
using HospitalLibrary.Invitations.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [ApiController]   
    [Authorize(Roles="Doctor")]
    [Route("api/intranet/invitations")]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationService _invitationService;

        public InvitationController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }

        [Route("all")]
        [HttpGet]
        public IActionResult GetAllInvitations()
        {
            var result = _invitationService.GetAllInvitations();
            return Ok(result);
        }
        [Route("create/all")]
        [HttpPost]
        public async Task<IActionResult> CreateNewTeamBuildingEventForEveryone([FromBody] CreateInvitationDto invitationDto){
            
        var result = await _invitationService.CreateEventForAll(invitationDto);
            return Ok(result);
        }

        [Route("create/special")]
        [HttpPost]
        public async Task<IActionResult> CreateNewTimeBuildingEventForSpecific([FromBody] CreateInvitationDto invitationDto)
        {
            var result = await _invitationService.CreateEventForSpeciality(invitationDto,invitationDto.SpecialityId);
            return Ok(result);
        }

        [Route("doctor")]
        [HttpGet]
        public IActionResult GetAllByDoctor()
        {
            int id = GetCurrentUser().Id;
            IEnumerable<Invitation> invitations = _invitationService.GetAllByDoctorId(id);
            return Ok(invitations);
        }

        [Route("accept/{invitationId}")]
        [HttpPatch]
        public IActionResult AcceptInvitation(int invitationId)
        {
            Invitation invitation = _invitationService.AcceptInvitation(invitationId);
            return Ok(invitation);
        }
        
        [Route("decline/{invitationId}")]
        [HttpPatch]
        public IActionResult DeclineInvitation(int invitationId)
        {
            Invitation invitation = _invitationService.DeclineInvitation(invitationId);
            return Ok(invitation);
        }

        
        private UserDTO GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserDTO
                {
                    Id = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                    Role = Role.Doctor,
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
                };
            }

            return null;
        }
    }
}