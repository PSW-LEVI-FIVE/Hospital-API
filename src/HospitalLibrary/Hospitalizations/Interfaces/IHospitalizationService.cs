using System.Threading.Tasks;
using HospitalLibrary.Hospitalizations.Dtos;

namespace HospitalLibrary.Hospitalizations.Interfaces
{
    public interface IHospitalizationService
    {
        public Hospitalization Create(Hospitalization hospObj);
        public Hospitalization EndHospitalization(int id, EndHospitalizationDTO dto);
        public Task<string> GenerateTherapyReport(int id);

    }
}