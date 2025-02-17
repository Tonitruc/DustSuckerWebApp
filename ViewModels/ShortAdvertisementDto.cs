namespace DustSuckerWebApp.ViewModels
{
    public class ShortAdvertisementDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string Status { get; set; }

        public DateTime PublishDate { get; set; }

        public List<string> ImageUrls { get; set; }

        public int HooverId { get; set; }


        public ShortAdvertisementDto()
        {
            Title = string.Empty;
            Description = string.Empty;
            Status = string.Empty;
            ImageUrls = [];
        }
    }
}
