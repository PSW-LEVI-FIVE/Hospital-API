using System;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Shared.Model.ValueObjects
{
    [Owned]
    public class Coordinates : ValueObject<Coordinates>
    {
        public float XCoordinate { get; set; }
        public float YCoordinate { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Coordinates(float xCoordinate, float yCoordinate, float width, float height)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Width = width;
            Height = height;
            Validate();
        }

        private void Validate()
        {
            if (XCoordinate < 0 || YCoordinate < 0 || Width < 0 || Height < 0)
                throw new Exception("Coordinate values must be positive!");
        }
        

        protected override bool EqualsCore(Coordinates other)
        {
            return XCoordinate.Equals(other.XCoordinate) &&
                   YCoordinate.Equals(other.YCoordinate) &&
                   Width.Equals(other.Width) &&
                   Height.Equals(other.Height);
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = XCoordinate.GetHashCode();
                hashCode = (hashCode * 397) ^ YCoordinate.GetHashCode();
                hashCode = (hashCode * 397) ^ Width.GetHashCode();
                hashCode = (hashCode * 397) ^ Height.GetHashCode();
                return hashCode;
            }
        }
        
    }
}