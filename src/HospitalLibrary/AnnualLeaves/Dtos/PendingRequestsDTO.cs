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

        public PendingRequestsDTO(int id, int doctorId, string doctor, string reason, DateTime startAt, DateTime endAt, AnnualLeaveState state, bool isUrgent)
        {
            Id = id;
            DoctorId = doctorId;
            Doctor = doctor;
            Reason = reason;
            StartAt = startAt;
            EndAt = endAt;
            State = state;
            IsUrgent = isUrgent;
        }
    }
}
