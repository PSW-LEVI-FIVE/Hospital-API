using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Dtos;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Public
{
    [Route("api/public/doctors")]
    [ApiController]
    public class DoctorController: ControllerBase
    {
        private IDoctorService _doctorService;
        private IEmailService _emailService;

        public DoctorController(IDoctorService doctorService, IEmailService emailService)
        {
            _doctorService = doctorService;
            _emailService = emailService;
        }
        
        [HttpGet]
        [Route("internal-medicine/registration")]
        public async Task<IActionResult> GetIternalMedicineDoctorsForPatientRegistration()
        {
            IEnumerable<Doctor> doctors = await _doctorService.GetIternalMedicineDoctorsForPatientRegistration();
            List <PatientsDoctorDTO> doctorDTOs = new List<PatientsDoctorDTO>();
            foreach (Doctor doctor in doctors)
            {
                PatientsDoctorDTO doctorDTO = new PatientsDoctorDTO(doctor.Name, doctor.Surname,doctor.Uid);
                doctorDTOs.Add(doctorDTO);
            }
            return Ok(doctorDTOs);
        }
        [HttpGet]
        [Route("step-by-step")]
        [Authorize(Roles="Patient")]
        public async Task<IActionResult> GetDoctorsForStepByStep()
        {
            UserDTO loggedUser = GetCurrentUser();
            IEnumerable<Doctor> doctors = await _doctorService.GetDoctorsForStepByStep(loggedUser.Id);
            List<DoctorWithSpecialityDTO> doctorsDTO = new List<DoctorWithSpecialityDTO>();
            foreach (Doctor doctor in doctors)
            {
                doctorsDTO.Add(new DoctorWithSpecialityDTO(doctor));
            }
            return Ok(doctorsDTO);
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
                    Role = Role.Patient,
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
                };
            }
            return null;
        }
    }
}