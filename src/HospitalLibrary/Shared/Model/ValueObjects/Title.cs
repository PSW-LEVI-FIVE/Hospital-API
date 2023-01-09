using System;
using System.Text.RegularExpressions;
using HospitalLibrary.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Shared.Model.ValueObjects
{
    [Owned]
    public class Title: ValueObject<Title>
    {
        public string TitleString { get; private set; }
        
        public Title(string titleString)
        {
            TitleString = titleString;
            Validate();
        }
        
        public Title(Title title)
        {
            TitleString = title.TitleString;
            Validate();
        }
        private void Validate()
        {
            if (TitleString.Trim().Equals(""))
            {
                throw new Exception("The title field should be filled!");
            }
        }
        
        protected override bool EqualsCore(Title other)
        {
            return TitleString.Equals(other.TitleString);
        }

        protected override int GetHashCodeCore()
        {
            int hashcode = TitleString.GetHashCode();
            return hashcode;
        }
    }
}