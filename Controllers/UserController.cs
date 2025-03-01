using DustSuckerWebApp.ServiceLayer.UserServices;
using DustSuckerWebApp.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;


        public UserController(UserService service)
        {
            _service = service;
        }


        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await _service.GetByEmail(email);
            if (result == null)
                return BadRequest("Invalid email");
            
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
                return BadRequest("Invalid id");

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserDto userDto)
        {
            try
            {
                var result = await _service.AddUser(userDto);
                return Ok(result);
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveById(int id)
        {
            var result = await _service.RemoveById(id);
            if(result)
                Ok(result);

            return BadRequest("Invalid id");
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> RemoveByEmail(string email)
        {
            var result = await _service.RemoveByEmail(email);
            if (result)
                Ok(result);

            return BadRequest("Invalid email");
        }

    }
}
