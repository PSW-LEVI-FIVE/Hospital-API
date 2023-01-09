using HospitalLibrary.Rooms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Examination.Dtos;
using HospitalLibrary.Renovations.Model;
using HospitalLibrary.Infrastructure.EventSourcing.Events;

namespace HospitalLibrary.Renovations.Interface
{
  public interface IRenovationService
  {
    Task<List<TimeInterval>> GenerateCleanerTimeSlots(TimeInterval timeInterval, int duration, int roomid);
    Task<List<Model.Renovation>> GetAllPending();
    Task<Model.Renovation> Create(Model.Renovation renovation);
    Task ExecuteRenovation(Model.Renovation renovation);
    Task<List<Model.Renovation>> GetAllPendingForRoom(int roomId);
    Model.Renovation CancelRenovation(int renovationId);
    Task<RenovationEventDTO> CreateEvent(Model.Renovation renovation);
    Task<Renovation> UpdateEvent(Renovation renovation, String uuid);
    void AddEvent(RenovationDomainEvent renovationDomainEvent);
  }
}
