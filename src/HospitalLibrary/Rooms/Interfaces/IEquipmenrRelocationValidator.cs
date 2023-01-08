using HospitalLibrary.AnnualLeaves.Dtos;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Model;


namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IEquipmenrRelocationValidator
    {
        Task Validate(EquipmentReallocation equipmentReallocation);
        void ThrowIfLessThan24hours(EquipmentReallocation equipmentReallocation);




    }
}