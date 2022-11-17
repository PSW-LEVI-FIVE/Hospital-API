using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Users
{
    public enum Role
    {
        Patient,
        Doctor,
        Manager,
        Secretary

    };
    public class User
    {
        [Key()]
        [ForeignKey("Person")]
        public int Id { get; set; }
        public Person Person { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public User()
        {
        }

        public User(string username, string password, Role role,int id)
        {
            Username = username;
            Password = password;
            Role = role;
            Id = id;
        }
        public User(int id, string username, string password,Role role)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.Role = role;
        }
    }
}