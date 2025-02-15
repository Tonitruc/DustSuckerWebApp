using DustSuckerWebApp.DataLayer;
using DustSuckerWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DustSuckerWebApp.ServiceLayer
{
    public class HooverService
    {
        private readonly AppDbContext _context;


        public HooverService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Hoover>> GetHoovers()
        {
            return await _context.Hoovers.ToListAsync();
        }

        public async Task<Hoover?> GetHoover(int id)
        {
            return await _context.Hoovers.SingleOrDefaultAsync(h => h.Id == id);
        }
    }
}
