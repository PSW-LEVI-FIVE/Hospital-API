
﻿using HospitalLibrary.Appointments;
using HospitalLibrary.Managers.Dtos;
using HospitalLibrary.Managers.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Managers
{
    public class ManagerService : IManagerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
    }
}
