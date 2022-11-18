<<<<<<< HEAD
<<<<<<< HEAD
﻿using HospitalLibrary.Appointments;
using HospitalLibrary.Managers.Dtos;
using HospitalLibrary.Managers.Interfaces;
=======
﻿using HospitalLibrary.Managers.Dtos;
>>>>>>> ff5b065 (added manager entity and tests for statistics of most popular doctors in hospital)
=======
﻿using HospitalLibrary.Appointments;
using HospitalLibrary.Managers.Dtos;
using HospitalLibrary.Managers.Interfaces;
>>>>>>> 019126d (fixed all comments on PR)
using HospitalLibrary.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Managers
{
<<<<<<< HEAD
<<<<<<< HEAD
    public class ManagerService : IManagerService
=======
    public class ManagerService
>>>>>>> ff5b065 (added manager entity and tests for statistics of most popular doctors in hospital)
=======
    public class ManagerService : IManagerService
>>>>>>> 019126d (fixed all comments on PR)
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

<<<<<<< HEAD
<<<<<<< HEAD
        public IEnumerable<DoctorWithPopularityDTO> GetMostPopularDoctorByAgeRange(int fromAge=0, int toAge=666,bool onlyMostPopularDoctors=false)
        {  
         Dictionary<int, List<int>> doctorPatientCombinations = new Dictionary<int, List<int>>();
            foreach (Appointment appointment in _unitOfWork.AppointmentRepository.GetAll().Result.ToList())
            {
                int age = (int)((DateTime.Now - _unitOfWork.PersonRepository.GetOne(appointment.PatientId).BirthDate).TotalDays / 365.242199);
                if (age > toAge || age < fromAge) continue;

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
            {
                foreach (List<int> list in doctorPatientCombinations.Values)
                {
                    if (list.Count > max) max = list.Count;
                }
            }
            foreach (int key in doctorPatientCombinations.Keys)
            {
                if (doctorPatientCombinations[key].Count == max && onlyMostPopularDoctors)
                {
                    mostPopularDoctors.Add(
                       new DoctorWithPopularityDTO(key, max, _unitOfWork.DoctorRepository.GetOne(key).Name, _unitOfWork.DoctorRepository.GetOne(key).Surname)
                    );
                }
                else if(!onlyMostPopularDoctors) mostPopularDoctors.Add(
                       new DoctorWithPopularityDTO(key, doctorPatientCombinations[key].Count, _unitOfWork.DoctorRepository.GetOne(key).Name, _unitOfWork.DoctorRepository.GetOne(key).Surname)
                    );
            }
            return mostPopularDoctors.AsEnumerable();
        }

=======
        public List<DoctorsPopularityDTO> GetMostPopularDoctorInRangeOfAge(int fromAge, int toAge)
        {
            throw new NotImplementedException();
=======
        public Task<IEnumerable<DoctorWithPopularityDTO>> GetMostPopularDoctorByAgeRange(int fromAge, int toAge)
        {  
         Dictionary<(int,int), int> doctorPatientCombinations = new Dictionary<(int, int), int>();
         Dictionary<int, int> doctorNumberPatients = new Dictionary<int, int>();
            doctorPatientCombinations.Add((3, 2), 5);
            Console.WriteLine(doctorPatientCombinations.First().ToString());
            Console.WriteLine("xd");
            foreach (Appointment appointment in _unitOfWork.AppointmentRepository.GetAll().Result.ToList())
            {
                Console.WriteLine(doctorPatientCombinations.First().ToString());
                //doctorPatientCombinations.ContainsKey((3, 2));
            }
            return null;
>>>>>>> 019126d (fixed all comments on PR)
        }

        public Task<IEnumerable<DoctorWithPopularityDTO>> GetMostPopularDoctors()
        {
            return null;
        }
>>>>>>> ff5b065 (added manager entity and tests for statistics of most popular doctors in hospital)
    }
}
