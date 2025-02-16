using Microsoft.EntityFrameworkCore;

namespace DustSuckerWebApp.DataLayer
{
    public class ValidationDbContextServiceProvider : IServiceProvider
    {
        private readonly DbContext _context;


        public ValidationDbContextServiceProvider(DbContext context)
        {
            _context = context;
        }


        public object? GetService(Type serviceType)
        {
            if(serviceType == typeof(DbContext))
            {
                return _context;
            }

            return null;
        }
    }
}
