namespace DustSuckerWebApp.ViewModels
{
    public class AdvertisementShortDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string Status { get; set; }

        public string PublishDate { get; set; }

        public string? MainImageUrl { get; set; }

        public int HooverId { get; set; }


        public AdvertisementShortDto()
        {
            Title = string.Empty;
            Description = string.Empty;
            Status = string.Empty;
        }
    }
}
