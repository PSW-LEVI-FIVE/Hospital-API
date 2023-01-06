using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Renovations.Model
{
  public class MergeDTO
  {
    [Required]
    public int MainRoomId { get; set; }
    [Required]
    public int SecondaryId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }


    public MergeDTO(int mainRoomId, int secondaryId, DateTime startDate, DateTime endTime)
    {
      MainRoomId = mainRoomId;
      SecondaryId = secondaryId;
      StartDate = startDate;
      EndDate = endTime;
    }
    public Renovation MapToModel()
    {
      return new Renovation()
      {
        MainRoomId = MainRoomId,
        SecondaryRoomId = SecondaryId,
        StartAt = StartDate,
        EndAt = EndDate,
        State = RenovationState.PENDING,
        Type = RenovationType.MERGE
      };
    }
  }
}
