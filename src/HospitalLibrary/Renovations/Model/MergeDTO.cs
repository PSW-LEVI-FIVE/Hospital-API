using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Renovations.Model
{
  public class MergeDTO
  {
    [Required]
    public int MainRoomId { get; set; }
    [Required]
    public string SecondaryIds { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }


    public MergeDTO(int mainRoomId, string secondaryIds, DateTime startDate, DateTime endTime)
    {
      MainRoomId = mainRoomId;
      SecondaryIds = secondaryIds;
      StartDate = startDate;
      EndDate = endTime;
    }
    public Renovation MapToModel()
    {
      
      
      return new Renovation()
      {
        MainRoomId = MainRoomId,
        SecondaryRoomIds = SecondaryIds,
        StartAt = StartDate,
        EndAt = EndDate,
        State = RenovationState.PENDING,
        Type = RenovationType.MERGE
      };
    }
  }
}
