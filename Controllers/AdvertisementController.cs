using DustSuckerWebApp.ServiceLayer;
using DustSuckerWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementController : ControllerBase
    {
        private readonly AdvertisementService _service;


        public AdvertisementController(AdvertisementService advertisementService)
        {
            _service = advertisementService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hooversList = await _service.GetAsync();

            return Ok(hooversList);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hoover = await _service.GetById(id);
            if (hoover == null)
            {
                return BadRequest("Invalid Id.");
            }

            return Ok(hoover);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddAdvertisementDto dto)
        {
            try
            {
                var result = await _service.AddAsync(dto);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

    }
}
