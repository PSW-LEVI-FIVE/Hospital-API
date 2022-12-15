using HospitalLibrary.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.AnnualLeaves.Dtos
{
    public class PendingRequestsDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Doctor { get; set; }
        public string Reason { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public AnnualLeaveState State { get; set; }
        public bool IsUrgent { get; set; }

        public PendingRequestsDTO(AnnualLeave annualLeave, Doctor doctor)
        {
            Id = annualLeave.Id;
            DoctorId = annualLeave.DoctorId;
            Doctor = doctor.Name+' '+doctor.Surname;
            Reason = annualLeave.Reason.Text;
            StartAt = annualLeave.StartAt;
            EndAt = annualLeave.EndAt;
            State = annualLeave.State;
            IsUrgent = annualLeave.IsUrgent;
        }
    }
}
