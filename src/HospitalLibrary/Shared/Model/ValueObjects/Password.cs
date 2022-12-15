using HospitalLibrary.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HospitalLibrary.Shared.Model.ValueObjects
{
    public class Password : ValueObject<Password>
    {
        public string PasswordString { get; private set; }

        public Password(string passwordString)
        {
            PasswordString = passwordString;
            Validate();
        }
        public Password(Password password)
        {
            PasswordString = password.PasswordString;
            Validate();
        }
        public Password(PasswordDTO password)
        {
            PasswordString = password.PasswordString;
            Validate();
        }
        private void Validate()
        {
            Regex validatePasswordRegex = new Regex("^(?=.*[0-9]).{6,}$");
            if (!validatePasswordRegex.IsMatch(PasswordString))
            {
                throw new Exception("Password has to have at least 6 characters and at least one number!");
            }
        }
        protected override bool EqualsCore(Password other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
