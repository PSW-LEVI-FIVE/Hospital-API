namespace HospitalLibrary.Users.Dtos
{
    public class UserDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
        
        public int Id { get; set; }
        
        public UserDTO()
        {
            
        }

        public UserDTO(string username, string password, Role role)
        {
            Username = username;
            Password = password;
            Role = role;
        }
        
        public UserDTO(string username, string password, Role role, int id)
        {
            Username = username;
            Password = password;
            Role = role;
            Id = id;
        }
    }
}