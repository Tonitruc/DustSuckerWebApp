using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int Rating { get; set; }

        public DateTime PublishDate { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Not empty")]
        [MaxLength(500, ErrorMessage = "Text must be less then 500 characters")]
        public string Text { get; set; }


        public Review()
        {
            Text = string.Empty;
        }
    }
}
