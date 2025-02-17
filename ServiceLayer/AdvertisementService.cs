using AutoMapper;
using DustSuckerWebApp.DataLayer;
using DustSuckerWebApp.Models;
using DustSuckerWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DustSuckerWebApp.ServiceLayer
{
    public class AdvertisementService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;


        public AdvertisementService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<ShortAdvertisementDto>> GetAsync()
        {
            return await _context.Advertisements
                .Include(ad => ad.Hoover)
                .Select(ad => 
            new ShortAdvertisementDto
            {
                Title = ad.Title,
                Description = ad.Description,
                HooverId = ad.HooverId,
                ImageUrls = ad.ImageUrls,
                Status = ad.Status,
                Cost = ad.Cost,
            }).ToListAsync();
        }

        public async Task<ShortAdvertisementDto?> GetById(int id)
        {
            var result = await _context.Advertisements
                .Include(e => e.Hoover)
                .FirstOrDefaultAsync(a => a.Id == id);
            if(result == null) return null;

            return new ShortAdvertisementDto
            {
                Title = result.Title,
                Description = result.Description,
                HooverId = result.HooverId,
                ImageUrls = result.ImageUrls,
                Status = result.Status,
                Cost = result.Cost,
            };
        }

        public async Task<AddAdvertisementDto?> AddAsync(AddAdvertisementDto dto)
        {
            var exist = await _context.Advertisements
                          .SingleOrDefaultAsync(e => e.Title == dto.Title);

            if (exist != null)
                throw new ValidationException("The title for one ad must be enique.");

            if (!DateTime.TryParseExact(dto.PublishDate, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                throw new ValidationException("Invalid date format. Example of correct date: dd.mm.yyyy HH:MM");

            var result = await _context.AddAsync(_mapper.Map<Advertisement>(dto));
            var errors = await _context.SaveChangeWithValidationAsync();
            if (!errors.IsEmpty)
                throw new ValidationException(errors.First().ErrorMessage);

            return dto;
        }
    }
}
