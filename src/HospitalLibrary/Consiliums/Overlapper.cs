using System.Collections.Generic;
using HospitalLibrary.Appointments;

namespace HospitalLibrary.Consiliums
{
    public class Overlapper
    {
        public int OverlappedNumber { get; set; }
        public List<int> OverlappedDoctorsList { get; set; }
        public TimeInterval BestConsiliumTime { get; set; }

        public Overlapper(int overlappedNumber, List<int> overlappedDoctorsList, TimeInterval bestConsiliumTime)
        {
            OverlappedNumber = overlappedNumber;
            OverlappedDoctorsList = overlappedDoctorsList;
            BestConsiliumTime = bestConsiliumTime;
        }
    }
}