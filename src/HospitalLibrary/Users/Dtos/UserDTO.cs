namespace HospitalLibrary.Users.Dtos
{
    public class UserDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }


        public UserDTO()
        {
            
        }

        public UserDTO(string username, string password, Role role)
        {
            Username = username;
            Password = password;
            Role = role;
        }
    }
}