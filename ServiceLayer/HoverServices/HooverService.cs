using AutoMapper;
using DustSuckerWebApp.DataLayer;
using DustSuckerWebApp.Models;
using DustSuckerWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.ServiceLayer.HoverServices
{
    public class HooverService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;


        public HooverService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<Hoover>> GetHoovers()
        {
            return await _context.Hoovers.ToListAsync();
        }

        public async Task<Hoover?> GetHoover(int id)
        {
            return await _context.Hoovers.SingleOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Hoover?> AddHoover(AddHooverDto dto)
        {
            var exist = await _context.Hoovers
                                      .SingleOrDefaultAsync(e => e.Model == dto.Model && e.Brand == dto.Brand);

            if (exist != null)
                throw new ValidationException("The model for one brand must be unique.");

            var result = await _context.AddAsync(_mapper.Map<Hoover>(dto));
            var errors = await _context.SaveChangeWithValidationAsync();
            if (!errors.IsEmpty)
                throw new ValidationException(errors.First().ErrorMessage);

            return result.Entity;
        }

        public async Task<bool> Remove(int id)
        {
            var exist = await _context.Hoovers
                .Include(e => e.Advertisements)
                .SingleOrDefaultAsync(e => e.Id == id);

            if (exist == null) return false;
            if (!exist.Advertisements.Any())
                throw new ValidationException("Hoover contains with advertisements");

            _context.Remove(exist);
            _context.SaveChanges();
            return true;
        }
    }
}
