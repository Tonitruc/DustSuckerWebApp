using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Models
{
    public class User : IdentityUser
    {
        public required string FullName { get; set; }
    }
}
