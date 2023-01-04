using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalLibrary.Advertisement
{
    public class Advertisement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Text { get; set; }
        
        public string PictureUrl { get; set; }
        
        public DateTime CreateDate { get; set; }

        public Advertisement()
        {
        }
        
        public Advertisement(int id, string title,string text,string pictureUrl,DateTime createDate)
        {
            Id = id;
            Title = title;
            Text = text;
            PictureUrl = pictureUrl;
            CreateDate = createDate;
        }
    }
}