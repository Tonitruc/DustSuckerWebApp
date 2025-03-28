using AutoMapper;
using DataLayer.EFCores;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ViewModels.ViewModels;

namespace ServiceLayer.UserServices
{
    public class UserService
    {
        private readonly AppDbContext _context;

        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;


        public UserService(AppDbContext context,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }


        public async Task<object> GetUserInfoAsync(string email)
        {
            return await _context.Users.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Email == email)
                ?? throw new ValidationException($"User with email {email} don't exists.");
        }

        public async Task<List<User>> GetUsersInfoAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<List<AdvertisementShortDto>> GetShortFavoriteAdvertisementByUserEmailAsync(string email)
        {
            var existUser = await _userManager.Users
                .Include(x => x.Cart)
                    .ThenInclude(f => f.Advertisements)
                .FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new ValidationException($"User with email {email} don't exists.");

            return existUser.Cart.Advertisements
                .Select(ad => _mapper.Map<AdvertisementShortDto>(ad))
                .ToList();
        }

        public async Task<List<AdvertisementDto>> GetFavoriteAdvertisementByUserEmailAsync(string email)
        {
            var existUser = await _userManager.Users
                .Include(x => x.Cart)
                    .ThenInclude(f => f.Advertisements)
                        .ThenInclude(a => a.Hoover)
                .FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new ValidationException($"User with email {email} don't exists.");

            return existUser.Cart.Advertisements
                .Select(ad => _mapper.Map<AdvertisementDto>(ad))
                .ToList();
        }

        public async Task<bool?> AddFavoriteAdvertisementByUserEmailAsync(string email, string title)
        {
            var existUser = await _userManager.Users
                .Include(x => x.Cart)
                .FirstOrDefaultAsync(u => u.Email == email)            
                ?? throw new ValidationException($"User with email {email} don't exists.");

            var existAdvertisement = await _context.Advertisements
                .FirstOrDefaultAsync(ad => ad.Title == title)
                ?? throw new ValidationException($"Advertisement with title {title} don't exists.");

            if(!existUser.Cart.Advertisements.Any(ad => ad.Id == existAdvertisement.Id))
                existUser.Cart.Advertisements.Add(existAdvertisement);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool?> DeleteFavoriteAdvertisementByUserEmailAsync(string email, string title)
        {
            var existUser = await _userManager.Users
                .Include(x => x.Cart)
                    .ThenInclude(c => c.Advertisements)
                .FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new ValidationException($"User with email {email} don't exists.");

            var existAdvertisement = await _context.Advertisements
                .FirstOrDefaultAsync(ad => ad.Title == title)
                ?? throw new ValidationException($"Advertisement with title \"{title}\" don't exists.");

            if (existUser.Cart.Advertisements.Any(ad => ad.Id == existAdvertisement.Id))
                existUser.Cart.Advertisements.Remove(existAdvertisement);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool?> AddFavoriteAdvertisementsByUserEmailAsync(string email, List<string> titles)
        {
            foreach(var title in titles)
                await AddFavoriteAdvertisementByUserEmailAsync(email, title);

            return true;
        }

        public async Task<bool?> DeleteFavoriteAdvertisementsByUserEmailAsync(string email, List<string> titles)
        {
            foreach (var title in titles)
                await DeleteFavoriteAdvertisementByUserEmailAsync(email, title);

            return true;
        }
    }
}
