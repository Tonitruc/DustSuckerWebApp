using ServiceLayer.UserServices;
using ViewModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace DustSuckerWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;


        public AuthController(AuthService service)
        {
            _service = service;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var res = await _service.RegisterUserAsync(model);
            if(res == null)
                return BadRequest("Email is exist, use other email!");

            return Ok(new { res.Succeeded, res.Errors });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model
            ,[FromServices] IConfiguration config)
        {
            var res = await _service.LoginUserAsync(model, config);
            return Ok(res);
        }

        //[Authorize]
        [HttpDelete("delete/{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            var res = await _service.DeleteUserAsync(email);
            return Ok(res);
        }
    }
}
