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
    public class DoctorService: IDoctorService
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

<<<<<<< HEAD
        public Task<IEnumerable<Doctor>> GetIternalMedicineDoctorsForPatientRegistration()
        {
            return _unitOfWork.DoctorRepository.GetTwoUnburdenedDoctors();
=======
        public IEnumerable<DoctorWithPopularityDTO> GetMostPopularDoctorByAgeRange(int fromAge = 0, int toAge = 666, bool onlyMostPopularDoctors = false)
        {
            return await _dataContext.Doctors
                .Where(doctor => doctor.SpecialtyType
                .Equals(SpecialtyType.ITERNAL_MEDICINE))
                .OrderBy(doctor => doctor.Patients.Count)
                .Include(a => a.Patients)
                .Take(2).ToListAsync();
            /*Dictionary<int, List<int>> doctorPatientCombinations = new Dictionary<int, List<int>>();
            foreach (Appointment appointment in _unitOfWork.AppointmentRepository.GetAll().Result.ToList())
            {
                int age = (int)((DateTime.Now - _unitOfWork.PersonRepository.GetOne(appointment.PatientId).BirthDate).TotalDays / 365.242199);
                
                if (age > toAge || age < fromAge) 
                    continue;

                if (!doctorPatientCombinations.ContainsKey(appointment.DoctorId))
                {
                    List<int> newList = new List<int>();
                    newList.Add(appointment.PatientId);
                    doctorPatientCombinations.Add(appointment.DoctorId, newList);
                }
                else if (!doctorPatientCombinations[appointment.DoctorId].Contains(appointment.PatientId))
                    doctorPatientCombinations[appointment.DoctorId].Add(appointment.PatientId);
            }

            List<DoctorWithPopularityDTO> mostPopularDoctors = new List<DoctorWithPopularityDTO>();
            int max = 0;

            if (onlyMostPopularDoctors)
                foreach (List<int> list in doctorPatientCombinations.Values)
                    if (list.Count > max) 
                        max = list.Count;
            
            foreach (int key in doctorPatientCombinations.Keys)
            {
                if (doctorPatientCombinations[key].Count == max && onlyMostPopularDoctors)
                {
                    Doctor doctor = _unitOfWork.DoctorRepository.GetOne(key);
                    mostPopularDoctors.Add(
                       new DoctorWithPopularityDTO(key, max, doctor.Name, doctor.Surname));
                }
                else if (!onlyMostPopularDoctors)
                {
                    Doctor doctor = _unitOfWork.DoctorRepository.GetOne(key);
                    mostPopularDoctors.Add(
                       new DoctorWithPopularityDTO(key, doctorPatientCombinations[key].Count, doctor.Name, doctor.Surname));
                }
            }
            return mostPopularDoctors.AsEnumerable();*/
>>>>>>> 039b3a0 (rebasing)
        }
    }
}