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
    }
}