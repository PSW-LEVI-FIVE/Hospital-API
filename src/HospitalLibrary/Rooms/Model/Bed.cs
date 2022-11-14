namespace HospitalLibrary.Rooms.Model
{
    public class Bed: Rooms.Model.RoomEquipment
    {

        
        public Bed(int id, int quantity, string name, int roomId) : base(id, quantity, name, roomId)
        {
        }
    }
}