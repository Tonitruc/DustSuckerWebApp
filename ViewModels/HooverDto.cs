using DustSuckerWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.ViewModels
{
    public class HooverShortDto
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Type { get; set; }

        public string DustBagType { get; set; }

        public string CleaningType { get; set; }

        public string PowerType { get; set; }

        public double Weight { get; set; }


        public HooverShortDto()
        {
            Brand = string.Empty;
            Model = string.Empty;
            Type = string.Empty;
            DustBagType = string.Empty;
            CleaningType = string.Empty;
            PowerType = string.Empty;
        }
    }
}
