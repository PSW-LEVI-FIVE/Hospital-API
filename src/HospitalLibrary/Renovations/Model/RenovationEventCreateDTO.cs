using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Renovations.Model
{
  public class RenovationEventCreateDTO
  {

    public int MainRoomId;

    public RenovationType type;

    public Renovation MapToModel()
    {
      return new Renovation(MainRoomId, type);
    }

  }
}
