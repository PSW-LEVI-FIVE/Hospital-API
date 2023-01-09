using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Renovations.Model
{
  public class RenovationEventCreateDTO
  {
    [Required]
    public int MainRoomId;
    [Required]
    public RenovationType Type;

    public Renovation MapToModel()
    {
      return new Renovation(MainRoomId, Type);
    }

  }
}
