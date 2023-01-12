namespace HospitalLibrary.Medicines
{
    public class MedicineSearchResultDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }


        public MedicineSearchResultDTO(Medicine medicine)
        {
            Id = medicine.Id;
            Name = medicine.Name.NameString;
            Quantity = medicine.Quantity;
        }

    }
}