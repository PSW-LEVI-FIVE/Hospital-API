using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.Invitations;
using HospitalLibrary.Invitations.Dtos;
using HospitalLibrary.Invitations.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/invitations")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationService _invitationService;

        public InvitationController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }

        [Route("create")]
        [HttpPost]
        IActionResult CreateNewTeamBuildingEvent([FromBody] CreateInvitationDto invitationDto)
        {

            return null;

        }

        [Route("doctor")]
        [HttpGet]
        IActionResult GetAllByDoctor()
        {
            int id = GetCurrentUser().Id;
            IEnumerable<Invitation> invitations = _invitationService.GetAllByDoctorId(id);
            return Ok(invitations);
        }

        [Route("accept/{invitationId}")]
        [HttpPatch]
        IActionResult AcceptInvitation(int invitationId)
        {
            int docId = GetCurrentUser().Id;
            Invitation invitation = _invitationService.AcceptInvitation(invitationId);
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