using HospitalLibrary.Appointments;

namespace HospitalLibrary.Consiliums
{
    public class TimeIntervalDoctor
    {
        public TimeInterval Interval { get; set; }
        public int DoctorId { get; set; }

        public TimeIntervalDoctor(TimeInterval interval, int doctorId)
        {
            Interval = interval;
            DoctorId = doctorId;
        }
    }
}