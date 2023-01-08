using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.BloodStorages.Dtos
{
    public class BloodStorageDto
    {
        [Required] public string BloodType { get; set; }
        [Required] public double Quantity { get; set; }

        public BloodStorageDto(BloodStorage blood)
        {
            BloodType = GetBloodTypeString((int)blood.BloodType);
            Quantity = blood.Quantity;
        }
        
        private string GetBloodTypeString(int bType)
        {
            string blood = "";
            switch (bType)
            {
                case 0:
                {
                    blood = "A positive";
                    break;
                }
                case 1:
                {
                    blood = "A negative";
                    break;
                }
                case 2:
                {
                    blood = "B positive";
                    break;
                }
                case 3:
                {
                    blood = "B negative";
                    break;
                }
                case 4:
                {
                    blood = "AB positive";
                    break;
                }
                case 5:
                {
                    blood = "AB negative";
                    break;
                }
                case 6:
                {
                    blood = "0 positive";
                    break;
                }
                case 7:
                {
                    blood = "0 negative";
                    break;
                }
            }
            return blood;
        }
    }
}