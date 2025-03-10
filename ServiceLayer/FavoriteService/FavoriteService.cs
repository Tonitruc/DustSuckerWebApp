using DataLayer.EFCores;

namespace ServiceLayer.FavoriteService
{
    public class FavoriteService
    {
        private readonly AppDbContext _context;


        public FavoriteService(AppDbContext context)
        {
            _context = context;
        }



    }
}
