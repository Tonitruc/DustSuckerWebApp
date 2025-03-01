using AutoMapper;
using DustSuckerWebApp.DataLayer;
using DustSuckerWebApp.Models;
using DustSuckerWebApp.ServiceLayer.AdvertisementsServices.QueryObject;
using DustSuckerWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DustSuckerWebApp.ServiceLayer.AdvertisementsServices
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

        public async Task<List<AdvertisementDto>> GetAsync(int? pageNum, int? pageSize, 
            string? sortedBy, 
            [FromQuery] AdvertisementFilterParameters queries)
        { 
            return await _context.Advertisements
                .AsNoTracking()
                .Include(ad => ad.Hoover)
                .FilterAdvertisementsBy(queries)
                .SortAdvertisementsBy(sortedBy)
                .Select(ad => _mapper.Map<AdvertisementDto>(ad)).ToListAsync();
        }

        public async Task<List<AdvertisementShortDto>> GetShortAsync(int? pageNum, int? pageSize,
            string? sortedBy,
            [FromQuery] AdvertisementFilterParameters queries)
        {
            return await _context.Advertisements
                .AsNoTracking()
                .Include(ad => ad.Hoover)
                .FilterAdvertisementsBy(queries)
                .SortAdvertisementsBy(sortedBy)
                .Select(ad => _mapper.Map<AdvertisementShortDto>(ad)).ToListAsync();
        }

        public async Task<AdvertisementDto?> GetByIdAsync(int id)
        {
            var exist = await _context.Advertisements
                .AsNoTracking()
                .Include(e => e.Hoover)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (exist == null) return null;

            return _mapper.Map<AdvertisementDto>(exist);
        }

        public async Task<AdvertisementDto?> GetByTitleAsync(string title)
        {
            var exist = await _context.Advertisements
                .AsNoTracking()
                .Include(e => e.Hoover)
                .FirstOrDefaultAsync(a => a.Title == title);
            if (exist == null) return null;

            return _mapper.Map<AdvertisementDto>(exist);
        }

        public async Task<AdvertisementShortDto?> GetShortByIdAsync(int id)
        {
            var exist = await _context.Advertisements
                .Include(e => e.Hoover)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (exist == null) return null;

            return _mapper.Map<AdvertisementShortDto>(exist);
        }

        public async Task<AdvertisementShortDto?> GetShortByTitleAsync(string title)
        {
            var exist = await _context.Advertisements
                .Include(e => e.Hoover)
                .FirstOrDefaultAsync(a => a.Title == title);
            if (exist == null) return null;

            return _mapper.Map<AdvertisementShortDto>(exist);
        }

        public async Task<List<string>?> AddImageUrlByIdAsync(int id, string imageUrl)
        {
            var exist = await _context.Advertisements
               .SingleOrDefaultAsync(ad => ad.Id == id);

            if (exist == null) return null;

            if (!exist.ImageUrls.Contains(imageUrl))
                exist.ImageUrls.Add(imageUrl);

            await _context.SaveChangesAsync();
            return exist.ImageUrls;
        }

        public async Task<List<string>?> RemoveImageUrlByIdAsync(int id, string imageUrl)
        {
            var exist = await _context.Advertisements
                .SingleOrDefaultAsync(ad => ad.Id == id);

            if (exist == null) return null;

            exist.ImageUrls.Remove(imageUrl);

            await _context.SaveChangesAsync();
            return exist.ImageUrls;
        }

        public async Task<List<string>?> AddImagesUrlByIdAsync(int id, List<string> imagesUrl)
        {
            var exist = await _context.Advertisements
                .SingleOrDefaultAsync(ad => ad.Id == id);

            if (exist == null) return null;

            foreach (var imageUrl in imagesUrl)
            {
                if (!exist.ImageUrls.Contains(imageUrl))
                    exist.ImageUrls.Add(imageUrl);
            }

            await _context.SaveChangesAsync();
            return exist.ImageUrls;
        }

        public async Task<bool?> SetAsMainImageByIdAsync(int id, string imageUrl)
        {
            var exist = await _context.Advertisements
                .SingleOrDefaultAsync(ad => ad.Id == id);

            if (exist == null) return null;

            exist.ImageUrls.Remove(imageUrl);
            exist.ImageUrls.Insert(0, imageUrl);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<string>?> AddImageUrlByTitleAsync(string title, string imageUrl)
        {
            var exist = await _context.Advertisements
               .SingleOrDefaultAsync(ad => ad.Title == title);

            if (exist == null) return null;

            if (!exist.ImageUrls.Contains(imageUrl))
                exist.ImageUrls.Add(imageUrl);

            return exist.ImageUrls;
        }

        public async Task<List<string>?> RemoveImageUrlByTitleAsync(string title, string imageUrl)
        {
            var exist = await _context.Advertisements
               .SingleOrDefaultAsync(ad => ad.Title == title);

            if (exist == null) return null;

            exist.ImageUrls.Remove(imageUrl);

            await _context.SaveChangesAsync();
            return exist.ImageUrls;
        }

        public async Task<bool?> SetAsMainImageByTitleAsync(string title, string imageUrl)
        {
            var exist = await _context.Advertisements
                .SingleOrDefaultAsync(ad => ad.Title == title);

            if (exist == null) return null;

            exist.ImageUrls.Remove(imageUrl);
            exist.ImageUrls.Insert(0, imageUrl);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AddAdvertisementDto?> AddAsync(AddAdvertisementDto dto)
        {
            var exist = await _context.Advertisements
                .SingleOrDefaultAsync(ad => ad.Title == dto.Title);

            if (exist != null)
                throw new ValidationException("The title for one ad must be enique.");

            if (!DateTime.TryParseExact(dto.PublishDate, "dd.MM.yyyy HH:mm",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                throw new ValidationException("Invalid date format. Example of correct date: dd.mm.yyyy HH:MM");

            var result = await _context.AddAsync(_mapper.Map<Advertisement>(dto));
            var errors = await _context.SaveChangeWithValidationAsync();
            if (!errors.IsEmpty)
                throw new ValidationException(errors.First().ErrorMessage);

            return dto;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var exist = await GetByIdAsync(id);

            if (exist == null) return false;

            _context.Remove(exist);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteByTitleAsync(string title)
        {
            var exist = await GetByTitleAsync(title);

            if (exist == null) return false;

            _context.Remove(exist);
            _context.SaveChanges();
            return true;
        }
    }
}
