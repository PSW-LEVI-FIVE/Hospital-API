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
            Regex validateNameRegex = new Regex("^[A-Z][a-z]+$");
            if (!validateNameRegex.IsMatch(TitleString))
            {
                throw new BadRequestException("Title cant be empty and has to start with an upper case!");
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