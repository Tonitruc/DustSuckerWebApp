using AutoMapper;
using DataLayer.EFCores;
using DataLayer.Models;
using ViewModels.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.HoverServices
{
    public class HooverService
    {
        private readonly AppDbContext _context;

        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;


        public HooverService(AppDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }


        public async Task<List<HooverDto>> GetHooversAsync()
        {
            return await _context.Hoovers.AsNoTracking().Include(h => h.Reviews).ThenInclude(r => r.User)
                .Select(h => _mapper.Map<HooverDto>(h)).ToListAsync();
        }

        public async Task<HooverDto> GetHooverByIdAsync(int id)
        {
            var exist = await _context.Hoovers
                .AsNoTracking()
                .Include(h => h.Reviews)
                .ThenInclude(r => r.User)
                .SingleOrDefaultAsync(h => h.Id == id);

            if (exist == null)
                throw new ValidationException($"Invalid id: {id}");

            return _mapper.Map<HooverDto>(exist);
        }

        public async Task<HooverDto> AddHooverAsync(AddHooverDto dto)
        {
            var exist = await _context.Hoovers
                .SingleOrDefaultAsync(e => e.Model == dto.Model && e.Brand == dto.Brand)
                ?? throw new ValidationException("The model for one brand must be unique.");

            var result = await _context.AddAsync(_mapper.Map<Hoover>(dto));
            var errors = await _context.SaveChangeWithValidationAsync();
            if (!errors.IsEmpty)
                throw new ValidationException(errors.First().ErrorMessage);

            return _mapper.Map<HooverDto>(exist);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var exist = await _context.Hoovers
                .Include(e => e.Advertisements)
                .SingleOrDefaultAsync(e => e.Id == id)
                ?? throw new ValidationException($"Invalid hoover id: {id}");

            if (exist == null) return false;
            if (exist.Advertisements.Count != 0)
                throw new ValidationException("Hoover contains with advertisements");

            _context.Remove(exist);
            _context.SaveChanges();
            return true;
        }

        public async Task<List<Review>> GetReviewsByIdAsync(int id)
        {
            var exist = await _context.Hoovers.AsNoTracking()
                .Include(h => h.Reviews)
                .SingleOrDefaultAsync(e => e.Id == id)
                 ?? throw new ValidationException($"Invalid hoover id: {id}");

            return exist.Reviews.ToList();
        }

        public async Task<bool> AddReviewsByIdAsync(int hooverId, AddReviewDto dto)
        {
            var exist = await _context.Hoovers
                .Include(h => h.Reviews)
                    .ThenInclude(r => r.User)
                .SingleOrDefaultAsync(h => h.Id == hooverId)
                 ?? throw new ValidationException($"Invalid hoover id: {hooverId}");

            if (exist.Reviews.Any(r => r.User.Email == dto.UserEmail))
                throw new ValidationException($"The user has already written a review about this product");

            var newReview = _mapper.Map<Review>(dto);
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == dto.UserEmail)
                ?? throw new ValidationException($"Invalid user email: {dto.UserEmail}");

            newReview.PublishDate = DateTime.Now;
            newReview.UserId = user.Id;
            exist.Reviews.Add(newReview);

            var errors = await _context.SaveChangeWithValidationAsync();
            if (!errors.IsEmpty)
                throw new ValidationException($"{errors.First()}");

            return true;
        }

        public async Task<bool> RemoveReviewsByIdAsync(int hooverId, string email)
        {
            var exist = await _context.Hoovers
                .Include(h => h.Reviews)
                    .ThenInclude(r => r.User)
                .SingleOrDefaultAsync(h => h.Id == hooverId)
                 ?? throw new ValidationException($"Invalid hoover id: {hooverId}");

            var userReview = exist.Reviews.FirstOrDefault(r => r.User.Email == email)
                ?? throw new ValidationException($"The user did not post a comment about this product.");

            exist.Reviews.Remove(userReview);

            var errors = await _context.SaveChangeWithValidationAsync();
            if (!errors.IsEmpty)
                throw new ValidationException($"{errors.First()}");

            return true;
        }
    }
}
