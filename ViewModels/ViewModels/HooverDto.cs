using DataLayer.Models;

namespace ViewModels.ViewModels
{
    public class HooverDto
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Type { get; set; }

        public string DustBagType { get; set; }

        public string CleaningType { get; set; }

        public string TubeType { get; set; }

        public double DustBagCapacity { get; set; }

        public string FilterType { get; set; }

        public double PowerConsumption { get; set; }

        public string PowerType { get; set; }

        public double BatteryCapacity { get; set; }

        public double BatteryLife { get; set; }

        public double CableLength { get; set; }

        public double SuctionPower { get; set; }

        public List<string> NozzlesIncluded { get; set; }

        public double Weight { get; set; }

        public int AmountReviews { get; set; }

        public double Rating { get; set; }


        public ICollection<Review> Reviews { get; set; }

        public HooverDto()
        {
            Brand = string.Empty;
            Model = string.Empty;
            Type = string.Empty;
            DustBagType = string.Empty;
            TubeType = string.Empty;
            CleaningType = string.Empty;
            FilterType = string.Empty;
            PowerType = string.Empty;
            NozzlesIncluded = [];
        }
    }
}

