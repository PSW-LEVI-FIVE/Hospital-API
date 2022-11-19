using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Appointments;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.AnnualLeaves
{
    public class AppointmentRescheduler : IAppointmentRescheduler
    {
        private IUnitOfWork _unitOfWork;

        private IEmailService _emailService;

        public AppointmentRescheduler(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }
        
        public async Task Reschedule(int doctorId, TimeInterval timeInterval)
        {
            Doctor doctor = _unitOfWork.DoctorRepository.GetOne(doctorId);
            
            IEnumerable<Appointment> doctorAppointments =
                await _unitOfWork.AppointmentRepository.GetAllDoctorAppointmentsForRange(doctorId, timeInterval);
            
            IEnumerable<Doctor> doctors =
                _unitOfWork.DoctorRepository.GetAllDoctorsWithSpecialityExceptId(doctor.SpecialtyType, doctorId);

            Dictionary<int, IEnumerable<Appointment>> otherDoctorsAppointments =
               await GetDoctorsAppointments(doctors, timeInterval);

            Dictionary<Appointment, int> appointmentsToReschedule = new Dictionary<Appointment, int>();
            List<Appointment> appointmentsToCancel = new List<Appointment>();
            foreach(Appointment appointment in doctorAppointments)
            {
                int foundDoctorId = FindDoctorForRescheduling(otherDoctorsAppointments, appointment);
                if (foundDoctorId == -1)
                {
                    appointmentsToCancel.Add(appointment);
                } else {
                    appointmentsToReschedule.Add(appointment, foundDoctorId);
                }
            }
            
            SaveAppointments(appointmentsToReschedule, appointmentsToCancel);
            SendEmails(appointmentsToCancel);
        }

        private async Task<Dictionary<int, IEnumerable<Appointment>>> GetDoctorsAppointments(IEnumerable<Doctor> doctors, TimeInterval range)
        {
            Dictionary<int, IEnumerable<Appointment>> dictionary = new Dictionary<int, IEnumerable<Appointment>>();
            foreach (var doctor in doctors)
            {
                dictionary.Add(doctor.Id, await _unitOfWork.AppointmentRepository.GetAllDoctorAppointmentsForRange(doctor.Id, range));
            }

            return dictionary;
        }

        private int FindDoctorForRescheduling(Dictionary<int, IEnumerable<Appointment>> doctorsAppointments,
            Appointment appointment)
        {
            foreach (var id in doctorsAppointments.Keys)
            {
                if (CanDoctorDoAppointment(doctorsAppointments[id], appointment))
                {
                    return id;
                }
            }

            return -1;
        }

        private bool CanDoctorDoAppointment(IEnumerable<Appointment> appointments, Appointment appointment)
        {
            return !appointments.Any(a =>
                new TimeInterval(appointment.StartAt, appointment.EndAt).IsOverlaping(
                    new TimeInterval(a.StartAt, a.EndAt)));
        }

        private void SaveAppointments(Dictionary<Appointment, int> appointments, List<Appointment> appointmentsToCancel)
        {
            foreach (var appointment in appointments.Keys)
            {
                appointment.DoctorId = appointments[appointment];
                _unitOfWork.AppointmentRepository.Update(appointment);
            }

            foreach (var appointment in appointmentsToCancel)
            {
                appointment.State = AppointmentState.DELETED;
                _unitOfWork.AppointmentRepository.Update(appointment);
            }

            _unitOfWork.AppointmentRepository.Save();
        }
        
        private void SendEmails(List<Appointment> appointmentsToCancel)
        {
            foreach (var appointment in appointmentsToCancel)
            {
                _emailService.SendAppointmentCanceledEmail(appointment.Patient.Email, appointment.StartAt);
            }
        }
    }
}