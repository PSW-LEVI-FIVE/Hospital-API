﻿using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Repository;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Settings;
using HospitalLibrary.Renovation.Interface;

namespace HospitalLibrary.Renovation.Repository
{
    internal class RenovationRepository : BaseRepository<Model.Renovation>,IRenovationRepository
    {
        public RenovationRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<Model.Renovation>> GetAllPending()
        {
            return await _dataContext.Renovations
                .Where(a => a.State == RenovationState.PENDING)
                .ToListAsync();
        }

        public async Task<List<Model.Renovation>> GetAllPendingForRange(TimeInterval interval)
        {
            return await _dataContext.Renovations
                .Where(a => a.State == RenovationState.PENDING)
                .Where(a=>interval.Start.Date.CompareTo(a.StartAt.Date) <= 0)
                .Where(a=> interval.End.Date.CompareTo(a.StartAt.Date) > 0)
                .ToListAsync();
        }

        public async Task<TimeInterval> GetLastPendingForDay(DateTime date)
        {
            return await _dataContext.Renovations
                .Where(a => a.StartAt.Date == date.Date)
                .Where(a=>a.State==RenovationState.PENDING)
                .OrderByDescending(a => a.StartAt)
                .Select(a => new TimeInterval(a.StartAt, a.EndAt))
                .FirstOrDefaultAsync();
        }
        public async Task<TimeInterval> GetFirstPendingForDay(DateTime date)
        {
            return await _dataContext.Renovations
                .Where(a => a.StartAt.Date == date.Date)
                .Where(a => a.State == RenovationState.PENDING)
                .OrderBy(a => a.StartAt)
                .Select(a => new TimeInterval(a.StartAt, a.EndAt))
                .FirstOrDefaultAsync();
        }
    }
}