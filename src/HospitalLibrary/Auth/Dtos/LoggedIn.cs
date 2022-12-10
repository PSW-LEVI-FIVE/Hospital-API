using HospitalLibrary.Users;

namespace HospitalLibrary.Auth.Dtos
{
    public class LoggedIn
    {
        public string AccessToken { get; set; }
        public Role Role { get; set; }
    }
}