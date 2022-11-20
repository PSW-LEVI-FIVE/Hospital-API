using HospitalLibrary.Managers.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Managers
{
    public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
    {
        protected ManagerRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}
