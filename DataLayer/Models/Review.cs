﻿using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Not empty")]
        [MaxLength(500, ErrorMessage = "Text must be less then 500 characters")]
        public string Text { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }

        public int HooverId { get; set; }


        public Review()
        {
            Text = string.Empty;
        }
    }
}
