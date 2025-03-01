using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.Models
{
    public static class HooverAttributes
    {
        public static readonly List<string> HooverTypes = new()
        {
            "Классический", // Classic
            "Автомобильный", // CarVacuum
            "Робот-пылесос", // CleanerRobot
            "Вертикальный", // Vertical
            "Промышленный" // Industrial
        };

        public static readonly List<string> DustBagTypes = new()
        {
            "Бумажный", // Paper
            "Синтетический", // Synthetic
            "Многоразовый", // Reusable
            "Антибактериальный" // Antibacterial
        };

        public static readonly List<string> CleaningTypes = new()
        {
            "Сухая", // Dry
            "Моющая", // Washing
            "Аквафильтр" // Aquafilter
        };

        public static readonly List<string> TubeTypes = new()
        {
            "Цельная", // Solid
            "Составная", // Composite
            "Телескопическая" // Telescopic
        };

        public static readonly List<string> FilterTypes = new()
        {
            "Пылесборник", // DustCollector
            "Моторный" // Motor
        };

        public static readonly List<string> PowerTypes = new()
        {
            "Аккумулятор", // Battery
            "Источник питания", // PowerSupply
            "Комбинированный" // Combined
        };

        public static readonly List<string> NozzleTypes = new()
        {
            "Для сухой уборки", // Dry
            "Для влажной уборки", // Wet
            "Для ковров", // Carpet
            "Для ровных покрытий", // Smooth
            "Для шерсти", // Wool
            "Строительные насадки" // Build
        };
    }

    public class Hoover : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A brand is required")]
        [MinLength(1, ErrorMessage = "A brand cannot be empty")]
        [MaxLength(50, ErrorMessage = "Brand must be less than 50 characters")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "A model is required")]
        [MinLength(1, ErrorMessage = "A model cannot be empty")]
        [MaxLength(50, ErrorMessage = "Model must be less than 50 characters")]
        public string Model { get; set; }

        [Required]
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


        public ICollection<Advertisement> Advertisements { get; set; }


        public Hoover()
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
            Advertisements = new List<Advertisement>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!HooverAttributes.HooverTypes.Contains(Type))
                yield return new ValidationResult("Bad hoover type");

            if (!HooverAttributes.DustBagTypes.Contains(DustBagType))
                yield return new ValidationResult("Bad dust bag type");

            if (!HooverAttributes.TubeTypes.Contains(TubeType))
                yield return new ValidationResult("Bad tube type");

            if (!HooverAttributes.CleaningTypes.Contains(CleaningType))
                yield return new ValidationResult("Bad cleaning type");

            if (!HooverAttributes.FilterTypes.Contains(FilterType))
                yield return new ValidationResult("Bad filter type");

            if (!HooverAttributes.PowerTypes.Contains(PowerType))
                yield return new ValidationResult("Bad power type");

            if (!NozzlesIncluded.All(n => HooverAttributes.NozzleTypes.Contains(n)))
                yield return new ValidationResult("Bad nozzle type");
        }
    }
}
