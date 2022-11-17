namespace HospitalLibrary.Medicines.Interfaces
{
    public interface IMedicineService
    {
        bool GiveMedicine(int medicine, double quantity);
    }
}