using DustSuckerWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.ViewModels
{
    public class AddAdvertisementDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string Status { get; set; }

        public string PublishDate { get; set; }

        public List<string> ImageUrls { get; set; }


        public AddHooverDto Hoover { get; set; } = null!;


        public AddAdvertisementDto()
        {
            Title = string.Empty;
            Description = string.Empty;
            Status = string.Empty;
            PublishDate = string.Empty;
            ImageUrls = [];
        }
    }
}
