using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Appointments
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(HospitalDbContext context) : base(context)
        {
        }

        public async Task<List<TimeInterval>> GetAllRoomTakenIntervalsForDate(int roomId, DateTime date)
        {
            return await _dataContext.Appointments
                .Where(a => a.RoomId == roomId)
                .Where(a => a.StartAt.Date.Equals(date.Date))
                .Where(a => a.State.Equals(AppointmentState.PENDING))
                .Select(a => new TimeInterval(a.StartAt, a.EndAt))
                .ToListAsync();
        }
       



        public async Task<IEnumerable<TimeInterval>> GetAllDoctorTakenIntervalsForDate(int doctorId, DateTime date)
        {
            return await _dataContext.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Where(a => a.StartAt.Date.Equals(date.Date))
                .Where(a => a.State.Equals(AppointmentState.PENDING))
                .Select(a => new TimeInterval(a.StartAt, a.EndAt))
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeInterval>> GetAllDoctorTakenIntervalsForTimeInterval(int doctorId, TimeInterval timeInterval)
        {
            return await _dataContext.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Where(a =>
                    timeInterval.Start.Date.CompareTo(a.StartAt.Date) <= 0
                    && timeInterval.End.Date.CompareTo(a.StartAt.Date) > 0)
                .Where(a => a.State != AppointmentState.DELETED)
                .Select(a => new TimeInterval(a.StartAt, a.EndAt))
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllDoctorUpcomingAppointments(int doctorId)
        {
            return await _dataContext.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Where(a => a.State.Equals(AppointmentState.PENDING))
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllRoomUpcomingAppointments(int roomId)
        {
            return await _dataContext.Appointments
                .Where((a => a.RoomId == roomId))
                .Where(a => a.State.Equals(AppointmentState.PENDING))
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllDoctorAppointmentsForRange(int doctorId, TimeInterval interval)
        {
            return await _dataContext.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Where(a =>
                    interval.Start.Date.CompareTo(a.StartAt.Date) <= 0
                    && interval.End.Date.CompareTo(a.StartAt.Date) > 0)
                .Where(a => a.State != AppointmentState.DELETED)
                .Include(a => a.Patient)
                .OrderBy(a => a.StartAt)
                .ToListAsync();
        }
        
        public int GetNumberOfDoctorAppointmentsForRange(int doctorId, TimeInterval interval)
        {
            return _dataContext.Appointments.Count(a =>
                    a.DoctorId == doctorId && a.State == AppointmentState.PENDING &&
                    interval.Start.Date.CompareTo(a.StartAt.Date) <= 0
                    && interval.End.Date.CompareTo(a.StartAt.Date) > 0);
        }

        public async Task<Appointment> GetById(int appointmentId)
        {
            return await _dataContext.Appointments
                .Where(a => a.Id == appointmentId)
                .Include(a => a.Patient)
                .Include(a => a.Room)
                .SingleAsync();
        }

        public async Task<IEnumerable<TimeInterval>>  GetAllDoctorTakenIntervalsForDateExcept(int doctorId, DateTime date, int ignore)
        {
            return await _dataContext.Appointments
                .Where(a => a.DoctorId == doctorId && a.Id != ignore)
                .Where(a => a.StartAt.Date.Equals(date.Date))
                .Where(a => a.State.Equals(AppointmentState.PENDING))
                .Select(a => new TimeInterval(a.StartAt, a.EndAt))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Appointment>>  GetAllPatientAppointments(int patientId)
        {
            return await _dataContext.Appointments
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Doctor)
                .Include(a => a.Room)
                .OrderByDescending(a => a.StartAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeInterval>> GetAllRoomTakenIntervalsForDateExcept(int roomId, DateTime date, int ignore)
        {
            return await  _dataContext.Appointments
                .Where(a => a.RoomId == roomId && a.Id != ignore)
                .Where(a => a.StartAt.Date.Equals(date.Date))
                .Where(a => a.State.Equals(AppointmentState.PENDING))
                .Select(a => new TimeInterval(a.StartAt, a.EndAt))
                .ToListAsync();
        }

        
    }
}