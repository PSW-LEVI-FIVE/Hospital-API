using HospitalLibrary.Appointments;
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
        }

        public Task<IEnumerable<DoctorWithPopularityDTO>> GetMostPopularDoctors()
        {
            return null;
        }
    }
}
