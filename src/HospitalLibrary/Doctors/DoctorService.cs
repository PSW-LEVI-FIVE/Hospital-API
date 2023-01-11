using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Managers.Dtos;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Doctors
{
    public class DoctorService : IDoctorService
    {
        private IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Doctor Create(Doctor doctor)
        {
            _unitOfWork.DoctorRepository.Add(doctor);
            _unitOfWork.DoctorRepository.Save();
            return doctor;
        }

        public Task<IEnumerable<Doctor>> GetAll()
        {
            return _unitOfWork.DoctorRepository.GetAll();
        }

        public Task<IEnumerable<Doctor>> GetDoctorsForStepByStep(int patientId)
        {
            return _unitOfWork.DoctorRepository.GetDoctorsForStepByStep(patientId);
        }

        public Task<IEnumerable<Doctor>> GetDoctorForPatientBySpeciality(int patientId, string speciality)
        {
            return _unitOfWork.DoctorRepository.GetDoctorForPatientBySpeciality(patientId, speciality);
        }

        public Task<Doctor> GetMostUnburdenedDoctor()
        {
            return _unitOfWork.DoctorRepository.GetMostUnburdenedDoctor();
        }

        public Task<Doctor> GetDoctorByUid(string doctorUid)
        {
            return _unitOfWork.DoctorRepository.GetDoctorByUid(doctorUid);
        }
        public Task<Doctor> GetDoctorById(int doctorId)
        {
            return _unitOfWork.DoctorRepository.GetDoctorById(doctorId);
        }

        public async Task<IEnumerable<Doctor>> GetIternalMedicineDoctorsForPatientRegistration()
        {
            Doctor mostUnburdened = await GetMostUnburdenedDoctor();
            return await _unitOfWork.DoctorRepository.GetUnburdenedDoctors(mostUnburdened.Patients.Count);
        }
        
        public Task<IEnumerable<Doctor>> GetDoctorsByAgeRange(int fromAge, int toAge)
        {
            DateTime dateFromAge = new DateTime(DateTime.Now.Year - fromAge, DateTime.Now.Month, DateTime.Now.Day);
            DateTime dateToAge = new DateTime(DateTime.Now.Year - toAge, DateTime.Now.Month, DateTime.Now.Day);
            return _unitOfWork.DoctorRepository.GetDoctorsByAgeRange(dateFromAge, dateToAge);
        }

        public async Task<IEnumerable<Speciality>> GetAllSpecialities()
        {
            return await _unitOfWork.DoctorRepository.GetAllSpecialitiesInUse();
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsBySpecialization(int specialtyId)
        {
            Speciality specialty = _unitOfWork.DoctorRepository.GetSpecializationById(specialtyId);
            return await _unitOfWork.DoctorRepository.GetAllDoctorsBySpecialization(specialty);
        }
    }
}