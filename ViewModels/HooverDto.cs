namespace DustSuckerWebApp.ViewModels
{
    public class HooverDto
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Type { get; set; }

        public double PowerSupply { get; set; }

        public double SuctionForce { get; set; }

        public List<string> CleaningTypes { get; set; }
    }
}
