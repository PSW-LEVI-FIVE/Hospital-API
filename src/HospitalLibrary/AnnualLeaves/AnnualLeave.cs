using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.AnnualLeaves
{
    public enum AnnualLeaveState
    {
        PENDING, APPROVED, DELETED, CANCELED
    }
    
    public class AnnualLeave : BaseEntity
    {
        [ForeignKey("Doctor")]
        public int DoctorId { get; private set; }
        public Doctor Doctor { get; private  set; }
        
        public Reason Reason { get; private set; }
        
        public DateTime StartAt { get; private set; }
        public DateTime EndAt { get; private set; }

        public AnnualLeaveState State { get; private set; }
        
        public bool IsUrgent { get; private set; }

        public AnnualLeave()
        {
            
        }

        public AnnualLeave(int id, int doctorId, Doctor doctor, string reason, DateTime startAt, DateTime endAt, AnnualLeaveState state, bool isUrgent)
        {
            Id = id;
            DoctorId = doctorId;
            Doctor = doctor;
            Reason = new Reason(reason);
            StartAt = startAt;
            EndAt = endAt;
            State = state;
            IsUrgent = isUrgent;
        }
        
        public AnnualLeave(int doctorId, Doctor doctor, string reason, DateTime startAt, DateTime endAt, AnnualLeaveState state, bool isUrgent)
        {
            DoctorId = doctorId;
            Doctor = doctor;
            Reason = new Reason(reason);
            StartAt = startAt;
            EndAt = endAt;
            State = state;
            IsUrgent = isUrgent;
        }

        public void DeleteAnnualLeave(int doctorId)
        {
            if(State!=AnnualLeaveState.PENDING)
                throw new BadRequestException("Annual Leave isn't PENDING,can not cancel it!");
            if(DoctorId!=doctorId)
                throw new BadRequestException("Doctor and Annual-Leave don't match!");
            State = AnnualLeaveState.DELETED;
        }

        public void ReviewAnnualLeave(AnnualLeaveState state, string reason)
        {
            if (State != AnnualLeaveState.PENDING)
                throw new BadRequestException("Annual leave isn't pending, can't review it!");
            if (State == AnnualLeaveState.CANCELED && reason == null)
                throw new BadRequestException("Can't reject annual leave request without reason!");
            State = state;
            if (state == AnnualLeaveState.CANCELED)
            {
                Reason = new Reason(reason);

            }
        }
        
        public bool IsValid()
        {
            return StartAt < EndAt && StartAt > DateTime.Today.AddDays(5);
        }
    }
    
}