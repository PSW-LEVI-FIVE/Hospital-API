using HospitalLibrary.Rooms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;

namespace HospitalLibrary.Renovations.Interface
{
  public interface IRenovationService
  {
    Task<List<TimeInterval>> GenerateCleanerTimeSlots(TimeInterval timeInterval, int duration, int roomid);
    Task<List<Model.Renovation>> GetAllPending();
    Task<Model.Renovation> Create(Model.Renovation renovation);
    Task<TimeInterval> GetLatest(DateTime date, int roomid);
    Task ExecuteRenovation(Model.Renovation renovation);
  }
}
