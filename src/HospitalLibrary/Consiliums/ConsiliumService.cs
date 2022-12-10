using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Consiliums.Dtos;
using HospitalLibrary.Consiliums.Interfaces;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Consiliums
{
    public class ConsiliumService : IConsiliumService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsiliumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Consilium> Create(Appointment appointment, List<int> doctors)
        {
            Consilium consilium = appointment.Consilium;
            _unitOfWork.ConsiliumRepository.Add(consilium);
            foreach (int doctorId in doctors)
            {
                Appointment doctorAppointment = new Appointment(appointment);
                doctorAppointment.DoctorId = doctorId;
                _unitOfWork.AppointmentRepository.Add(doctorAppointment);
            }
            _unitOfWork.ConsiliumRepository.Save();
            _unitOfWork.AppointmentRepository.Save();
            return consilium;
        }
    }
}