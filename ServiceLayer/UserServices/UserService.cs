using AutoMapper;
using DustSuckerWebApp.DataLayer;
using DustSuckerWebApp.Models;
using DustSuckerWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.ServiceLayer.UserServices
{
    public class UserService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;


        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public List<User> Get()
        {
            return _context.Users.ToList();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(e => e.Email == email);
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(e => e.Id == e.Id);
        }

        public async Task<User?> AddUser(AddUserDto dto)
        {
            var exist = await GetByEmail(dto.Email);

            if (exist != null)
                throw new ValidationException("Email is exist.");

            var newUser = _mapper.Map<User>(dto);
            await _context.AddAsync(newUser);
            var error = await _context.SaveChangeWithValidationAsync();

            if (!error.IsEmpty)
                throw new ValidationException(error.First().ErrorMessage);

            return newUser;
        }

        public async Task<bool> RemoveByEmail(string email)
        {
            var exist = await GetByEmail(email);

            if (exist == null)
                return false;

            _context.Remove(exist);

            return true;
        }

        public async Task<bool> RemoveById(int id)
        {
            var exist = await GetById(id);

            if (exist == null)
                return false;

            _context.Remove(exist);

            return true;
        }
    }
}
