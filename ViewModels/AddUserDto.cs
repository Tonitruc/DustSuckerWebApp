using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.ViewModels
{
    public class AddUserDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public AddUserDto()
        {
            Name = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            PhoneNumber = string.Empty;
        }
    }
}
