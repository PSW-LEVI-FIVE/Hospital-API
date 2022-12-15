using HospitalLibrary.Shared.Dtos;
using HospitalLibrary.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HospitalLibrary.Shared.Model.ValueObjects
{
    [Owned]
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
        private void Validate()
        {
            Regex validatePasswordRegex = new Regex("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{6,}$");
            if (!validatePasswordRegex.IsMatch(PasswordString))
            {
                System.Console.WriteLine(PasswordString);
                throw new BadRequestException("Password has to have at least 6 characters and at least one number!");
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
