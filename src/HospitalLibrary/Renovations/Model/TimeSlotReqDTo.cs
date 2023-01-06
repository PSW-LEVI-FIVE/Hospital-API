using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Renovations.Model
{
  public class TimeSlotReqDTo
  {
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    [Required] public int RoomId { get; set; }
    [Required] public int Duration { get; set; }

  }
}
