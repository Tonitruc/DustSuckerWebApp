using DustSuckerWebApp.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace DustSuckerWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HooverController : ControllerBase
    {
        private readonly HooverService _service;


        public HooverController(HooverService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hooversList = await _service.GetHoovers();

            return Ok(hooversList);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hoover = await _service.GetHoover(id);
            if(hoover == null)
            {
                return BadRequest("Bad id.");
            }

            return Ok(hoover);
        }
    }
}
