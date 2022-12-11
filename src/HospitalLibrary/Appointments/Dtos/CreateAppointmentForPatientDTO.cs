using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Appointments.Dtos
{
    public class CreateAppointmentForPatientDTO
    {
        public string DoctorUid { get; set; }
        public TimeInterval ChosenTimeInterval { get; set; }

        public CreateAppointmentForPatientDTO(string doctorUid, TimeInterval chosenTimeInterval)
        {
            DoctorUid = doctorUid;
            ChosenTimeInterval = chosenTimeInterval;
        }

        public CreateAppointmentForPatientDTO()
        {
        }

        public Appointment MapToModel()
        {
            return new Appointment
            {
                StartAt = ChosenTimeInterval.Start,
                EndAt = ChosenTimeInterval.End,
                State = AppointmentState.PENDING
            };
        }
    }
}