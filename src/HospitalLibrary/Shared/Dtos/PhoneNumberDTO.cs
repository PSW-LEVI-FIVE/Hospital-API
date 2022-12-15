using HospitalLibrary.Shared.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Shared.Dtos
{
    public class PhoneNumberDTO
    {
        public string CellNumber { get; private set; }

        public PhoneNumber MapToModel()
        {
            return new PhoneNumber(CellNumber);
        }

        public PhoneNumberDTO()
        {
        }

        public PhoneNumberDTO(string cellNumber)
        {
            CellNumber = cellNumber;
        }
    }
}
