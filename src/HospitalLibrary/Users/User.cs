using System;
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
    public enum ActiveStatus
    {
        Active,
        Pending
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
        public string ActivationCode { get; set; }
        public ActiveStatus ActiveStatus { get; set; }
        public Boolean Blocked { get; set; }

        public User()
        {
        }
        public void ActivateAccount(string code)
        {
            if(code.Equals(ActivationCode))
                ActiveStatus = ActiveStatus.Active;
            else
                throw new Exception("Code not valid");
            
        }

        public User(string username, string password, Role role,int id,ActiveStatus activeStatus)
        {
            Username = username;
            Password = password;
            Role = role;
            Id = id;
            ActiveStatus = activeStatus;
            Blocked = false;
        }
        private void Validate()
        {
            if (Username.Trim().Equals("")
                || Password.Trim().Equals(""))
            {
                throw new Exception("Username and password cannot be empty!");
            }
        }
    }
}