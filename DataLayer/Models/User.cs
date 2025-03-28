using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Models
{
    public class User : IdentityUser
    {
        public required string FullName { get; set; }


        public int FavoriteId { get; set; }
        [Required]
        public required Cart Cart { get; set; } 

        public ICollection<Review> Reviews { get; set; }
    }
}
