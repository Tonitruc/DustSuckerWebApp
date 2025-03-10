using ServiceLayer.UserServices;
using ViewModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var res = await _service.RegisterUserAsync(model);
            if(res == null)
                return BadRequest("Email is exist, use other email!");

            return Ok(new { res.Succeeded, res.Errors });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model
            ,[FromServices] IConfiguration config)
        {
            try
            {
                var res = await _service.LoginUserAsync(model, config);
                if(res == null)
                    return BadRequest($"User with {model.Email} don't exists.");

                return Ok(res);

            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
