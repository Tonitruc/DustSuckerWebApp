﻿namespace ViewModels.ViewModels
{
    public class LoginUserResponse
    {
        public string JwtToken { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
