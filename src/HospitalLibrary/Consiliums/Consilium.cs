using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalLibrary.Consiliums
{
    public class Consilium
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public Consilium(string title)
        {
            Title = title;
        }
    }
}