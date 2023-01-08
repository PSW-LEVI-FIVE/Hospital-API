namespace HospitalLibrary.Advertisement.Dtos
{
    public class AdvertisementDTO
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string PictureUrl { get; set; }
        
        public AdvertisementDTO()
        {
        }
        
        public AdvertisementDTO(string title,string text, string pictureUrl)
        {
            Title = title;
            Text = text;
            PictureUrl = pictureUrl;
        }
    }
}