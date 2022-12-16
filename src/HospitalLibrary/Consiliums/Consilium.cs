using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Consiliums
{
    public class Consilium
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        public Title Title { get; set; }
        
        public Consilium(Title title)
        {
            Title = title;
        }
        
        public Consilium(string title)
        {
            Title = new Title(title);
        }

        public Consilium()
        {
        }

    }
}