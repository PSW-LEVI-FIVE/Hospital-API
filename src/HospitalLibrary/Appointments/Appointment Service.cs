using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Appointments
{
    public class AppointmentService:IAppointmentService
    {
        private IUnitOfWork _unitOfWork;
        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Appointment>> GetAll()
        {
            return _unitOfWork.AppointmentRepository.GetAll();
        }

        public Appointment Create(Appointment appointment)
        {
            _unitOfWork.AppointmentRepository.Add(appointment);
            _unitOfWork.AppointmentRepository.Save();
            return appointment;
        }

        public async Task<IEnumerable<Appointment>> GetUpcomingForDoctor(Doctor doctor)
        {
            return await _unitOfWork.AppointmentRepository.GetAllDoctorUpcomingAppointments(doctor.Id);
        }
    }
}