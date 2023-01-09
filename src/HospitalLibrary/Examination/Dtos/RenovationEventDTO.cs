using HospitalLibrary.Rooms.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Renovations.Model;

namespace HospitalLibrary.Examination.Dtos
{
  public class RenovationEventDTO
  {
    public int Id { get; set; }
    public int MainRoomId { get; set; }
    public int? SecondaryRoomId { get; set; }
    public RenovationType Type { get; set; }
    public RenovationState State { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public String? roomName { get; set; }
    public String Uuid { get; set; }

    public RenovationEventDTO(Renovation renovation, string uuid)
    {
      MainRoomId = renovation.MainRoomId;
      StartAt = renovation.StartAt;
      EndAt = renovation.EndAt;
      State = RenovationState.PENDING;
      Type = RenovationType.SPLIT;
      SecondaryRoomId = renovation.SecondaryRoomId;
      roomName = renovation.roomName;
      Uuid=uuid;
    }

  }
}
