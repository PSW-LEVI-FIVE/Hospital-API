using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Allergens.Dtos
{
    public class AllergenWithNumberPatientsDTO
    {
        public string Allergen { set; get; }

        public int Patients { set; get; }

        public AllergenWithNumberPatientsDTO (string allergen, int patients)
        {
            this.Allergen = allergen;
            this.Patients = patients;
        }
    }
}
