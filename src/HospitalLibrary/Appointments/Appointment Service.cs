using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Appointments
{
    public class AppointmentService:IAppointmentService
    {
        private IUnitOfWork _unitOfWork;
        private ITimeIntervalValidationService _intervalValidation;
        public AppointmentService(IUnitOfWork unitOfWork, ITimeIntervalValidationService intervalValidation)
        {
            _unitOfWork = unitOfWork;
            _intervalValidation = intervalValidation;
        }

        public Task<IEnumerable<Appointment>> GetAll()
        {
            return _unitOfWork.AppointmentRepository.GetAll();
        }

        public async Task<Appointment> Create(Appointment appointment)
        {
            await _intervalValidation.ValidateAppointment(appointment);
            _unitOfWork.AppointmentRepository.Add(appointment);
            _unitOfWork.AppointmentRepository.Save();
            return appointment;
        }

    }
}