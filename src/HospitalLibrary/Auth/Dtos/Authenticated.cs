using HospitalLibrary.Users;

namespace HospitalLibrary.Auth.Dtos
{
    public class Authenticated
    {
        public string Username { get; set; }
        public Role Role { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}