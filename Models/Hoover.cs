using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.Models
{
    public class Hoover
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A brand is required")]
        [MinLength(1, ErrorMessage = "A brand cannot be empty")]
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Type { get; set; }

        public double PowerSupply { get; set; }

        public double SuctionForce { get; set; }

        public List<string> CleaningTypes { get; set; }


        public Hoover()
        {
            Brand = string.Empty;
            Model = string.Empty;
            Type = string.Empty;
            CleaningTypes = [];
        }
    }
}
