using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; }

        public Cart()
        {
            Advertisements = [];
        }
    }
}
