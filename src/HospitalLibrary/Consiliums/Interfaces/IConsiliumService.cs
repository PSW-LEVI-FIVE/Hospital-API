using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Consiliums.Dtos;

namespace HospitalLibrary.Consiliums.Interfaces
{
    public interface IConsiliumService
    {
        Task<Consilium> Create(Appointment appointment, List<int> doctors);

        GetBestConsiliumsDTO SuggestConsilium(TimeInterval timeInterval, List<int> doctors, int schedulerDoctor, int consiliumDuration);
    }
}