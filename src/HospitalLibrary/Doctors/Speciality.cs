using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalLibrary.Doctors
{
    public class Speciality
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public Speciality()
        {
            
        }

        public Speciality(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}