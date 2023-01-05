namespace HospitalLibrary.Advertisement.Dtos
{
    public class CreateAdvertisementDto
    {
        public string Title { get; set; }
        
        public string Text { get; set; }
        
        public string PictureUrl { get; set; }
        
        public CreateAdvertisementDto()
        {
        }
        
        public CreateAdvertisementDto(string title,string text, string pictureUrl)
        {
            Title = title;
            Text = text;
            PictureUrl = pictureUrl;
        }
    }
}