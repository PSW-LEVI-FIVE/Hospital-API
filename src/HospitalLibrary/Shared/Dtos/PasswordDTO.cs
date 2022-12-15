using HospitalLibrary.Shared.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Shared.Dtos
{
    public class PasswordDTO
    {
        public string PasswordString { get; set; }

        public Password MapToModel()
        {
            return new Password(PasswordString);
        }

        public PasswordDTO()
        {
        }

        public PasswordDTO(string passwordString)
        {
            PasswordString = passwordString;
        }
    }
}
