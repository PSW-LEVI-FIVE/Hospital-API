using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Appointments;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.AnnualLeaves
{
    public class AppointmentRescheduler : IAppointmentRescheduler
    {
        private IUnitOfWork _unitOfWork;

        public AppointmentRescheduler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task Reschedule(int doctorId, TimeInterval timeInterval)
        {
            Doctor doctor = _unitOfWork.DoctorRepository.GetOne(doctorId);
            
            IEnumerable<Appointment> doctorAppointments =
                await _unitOfWork.AppointmentRepository.GetAllDoctorAppointmentsForRange(doctorId, timeInterval);
            
            IEnumerable<Doctor> doctors =
               await _unitOfWork.DoctorRepository.GetAllDoctorsWithSpecialityExceptId(doctor.SpecialtyType, doctorId);

            Dictionary<int, IEnumerable<Appointment>> otherDoctorsAppointments =
               await GetDoctorsAppointments(doctors, timeInterval);

            Dictionary<Appointment, int> appointmentsToReschedule = new Dictionary<Appointment, int>();

            foreach(Appointment appointment in doctorAppointments)
            {
                appointmentsToReschedule.Add(appointment, FindDoctorForRescheduling(otherDoctorsAppointments, appointment));
            }
            
            SaveAppointments(appointmentsToReschedule);
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

            throw new BadRequestException("There are no available doctors in period from " + appointment.StartAt + " to " + appointment.EndAt);
        }

        private bool CanDoctorDoAppointment(IEnumerable<Appointment> appointments, Appointment appointment)
        {
            return !appointments.Any(a =>
                new TimeInterval(appointment.StartAt, appointment.EndAt).IsOverlaping(
                    new TimeInterval(a.StartAt, a.EndAt)));
        }

        private void SaveAppointments(Dictionary<Appointment, int> appointments)
        {
            foreach (var appointment in appointments.Keys)
            {
                appointment.DoctorId = appointments[appointment];
                _unitOfWork.AppointmentRepository.Add(appointment);
            }

            _unitOfWork.AppointmentRepository.Save();
        }
    }
}