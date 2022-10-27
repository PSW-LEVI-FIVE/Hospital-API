using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Patients;
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

        public SendEmailDto CancelAppointment(int appointmentId)
        {
            Appointment appointment = new Appointment{Id = appointmentId };
            Appointment canceled=_unitOfWork.AppointmentRepository.GetOne(appointment);
            canceled.State = AppointmentState.DELETED;
            Patient toNotify=_unitOfWork.PatientRepository.GetOne(canceled.Patient);
            SendEmailDto retDto=new SendEmailDto{ patientEmail = toNotify.Email, appointmentTime = canceled.StartAt.ToString()} ;
            _unitOfWork.AppointmentRepository.Update(canceled);
            _unitOfWork.AppointmentRepository.Save();
            
            return retDto;
        }
    }
}