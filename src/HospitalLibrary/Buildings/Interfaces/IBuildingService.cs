using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HospitalLibrary.Buildings.Interfaces
{
    public interface IBuildingService
    {
        Task<IEnumerable<Building>> GetAll();
        Building UpdateBuildingData(Building building);
    }
}