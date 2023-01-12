using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Shared.Model.ValueObjects
{
    public class CoordinatesList : ValueObject<CoordinatesList>, IEnumerable<Coordinates>
    {
        private List<Coordinates> _coordinates { get; }

        public List<Coordinates> Coordinates
        {
            get => _coordinates;
        }

        public CoordinatesList(IEnumerable<Coordinates> coordinates)
        {
            _coordinates = coordinates.ToList();
        }

        protected override bool EqualsCore(CoordinatesList other)
        {
            return _coordinates
                .OrderBy(x => x.XCoordinate)
                .SequenceEqual(other._coordinates.OrderBy(x => x.XCoordinate));
        }

        protected override int GetHashCodeCore()
        {
            return _coordinates.Count();
        }

        public IEnumerator<Coordinates> GetEnumerator()
        {
            return _coordinates.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static explicit operator CoordinatesList(string coordinatesList)
        {
            List<Coordinates> coordinates = coordinatesList.Split(";")
                .Select(x => (Coordinates)x)
                .ToList();

            return new CoordinatesList(coordinates);
        }

        public static implicit operator string(CoordinatesList coordinatesList)
        {
            return string.Join(";", coordinatesList.Select(x => (string)x));
        }
    }
}