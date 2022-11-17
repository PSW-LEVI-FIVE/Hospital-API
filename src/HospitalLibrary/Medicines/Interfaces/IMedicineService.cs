namespace HospitalLibrary.Medicines.Interfaces
{
    public interface IMedicineService
    {
        bool SubtractQuantity(int medicine, double quantity);
    }
}