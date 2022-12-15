using System;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Shared.Model.ValueObjects
{
    [Owned]
    public class Reason:ValueObject<Reason>
    {
        public string Text { get; private set; }

        public Reason(string text)
        {
            Text = text;
            Validate();
        }

        private void Validate()
        {
            if (Text.Trim().Equals(""))
                throw new Exception("Reason should not be empty!");
            
        }
        
        protected override bool EqualsCore(Reason other)
        {
            return Text.Equals(other.Text);
        }

        protected override int GetHashCodeCore()
        {
            int hashCode = Text.GetHashCode();
            return hashCode;
        }
    }
}