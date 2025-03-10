using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; }

        public Favorite()
        {
            Advertisements = [];
        }
    }
}
