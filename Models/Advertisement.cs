using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.Models
{
    public static class AdvertisementsAttributes
    {
        public static List<string> AddvertisementsStatus = [
            "Active",
            "Disable",
            "Hide"
            ];
    }

    public class Advertisement : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "A title cannot be empty")]
        [MaxLength(100, ErrorMessage = "Title must be less than 100 characters")]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = "Description must be less than 500 characters")]
        public string Description { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Bad data format. Example: 2025-02-16T00:00:00")]
        public DateTime PublishDate { get; set; }

        public List<string> ImageUrls { get; set; }


        public int HooverId { get; set; }
        public Hoover Hoover { get; set; } 


        public Advertisement()
        {
            Title = string.Empty;
            Description = string.Empty;
            Status = string.Empty;
            ImageUrls = [];
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!AdvertisementsAttributes.AddvertisementsStatus
                        .Any(s => s.Equals(Status, StringComparison.OrdinalIgnoreCase)))
            {
                yield return new ValidationResult("Ad status can accept one of the states: Hide, Disable, Active");
            }
        }
    }
}
