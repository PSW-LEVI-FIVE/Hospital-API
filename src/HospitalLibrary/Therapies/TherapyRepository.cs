using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Therapies.Interfaces;
using HospitalLibrary.Therapies.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace HospitalLibrary.Therapies
{
    public class TherapyRepository: BaseRepository<Therapy>, ITherapyRepository
    {
        public TherapyRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public  IEnumerable<Therapy> GetAllByHospitalization(int hospitalizationId)
        {
            return  _dataContext.Therapies
                .Where(t => t.HospitalizationId == hospitalizationId)
                .ToList();
        }

        public  List<Therapy> GetAllBloodTherapy()
        {
            return _dataContext.Therapies
                .Where(therapy => therapy.InstanceType.Equals("blood"))
                .ToList();
        }
        
    }
}