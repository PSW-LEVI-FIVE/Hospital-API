using System.Threading.Tasks;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Renovations.Interface
{
    public interface IRenovationValidator
    {
        
        
        Task Validate(Renovations.Model.Renovation renovation);
        void ThrowIfLessThan24hours(Renovations.Model.Renovation renovation);
        
    }
}