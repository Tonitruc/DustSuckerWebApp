using AutoMapper;
using DataLayer.EFCores;
using DataLayer.Models;
using ViewModels.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ServiceLayer.UserServices
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;


        private readonly IMapper _mapper;


        public AuthService(AppDbContext context, 
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IdentityResult?> RegisterUserAsync(RegisterViewModel model)
        {
            var existUser = await _userManager.FindByEmailAsync(model.Email);
            
            if(existUser != null) return null;

            existUser = new()
            {
                UserName = model.Username,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            var res = await _userManager.CreateAsync(existUser, model.Password);
/*            if(res.Succeeded)
            {
                await _context.AddAsync(existUser.Favorite);
                await _context.SaveChangesAsync();
            }*/

            return res;
        }

        public async Task<LoginUserResponse?> LoginUserAsync(LoginViewModel model
            ,IConfiguration config)
        {
            if(model.Email == null || model.Password == null)
                throw new ValidationException("Cant be empty data!");

            var existUser = await _userManager.FindByEmailAsync(model.Email);

            if (existUser == null) return null;

            var res = await _userManager.CheckPasswordAsync(existUser, model.Password);

            if (!res)
                throw new ValidationException("Invalid password!");

            return new() { JwtToken = GenerateJwtToken(existUser, config), 
                Email = existUser.Email ?? string.Empty,
                Username = existUser.UserName ?? string.Empty,
                PhoneNumber = existUser.PhoneNumber,
                FullName = existUser.FullName
            };
        }

        private string GenerateJwtToken(User user, IConfiguration config)
        {
            var jwtSettings = config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
