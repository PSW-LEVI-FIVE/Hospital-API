using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Hospitalizations.Interfaces
{
    public interface IHospitalizationService
    {
        public Hospitalization Create(Hospitalization hospObj);
        public Hospitalization EndHospitalization(int id, DateTime endTime);
        public Task<string> GenerateTherapyReport(int id);

        public Task<IEnumerable<Hospitalization>> GetAllForPatient(int id);

    }
}