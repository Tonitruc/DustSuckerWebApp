using ServiceLayer.HoverServices;
using ViewModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace DustSuckerWebApi.Controllers
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
            var hooversList = await _service.GetHooversAsync();
            return Ok(hooversList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hoover = await _service.GetHooverByIdAsync(id);
            return Ok(hoover);
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddHooverDto dto)
        {
            var result = await _service.AddHooverAsync(dto);
            return Ok(result);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _service.DeleteByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}/reviews")]
        public async Task<IActionResult> GetReviewsByid(int id)
        {
            var result = await _service.GetReviewsByIdAsync(id);
            return Ok(result);
        }

        //[Authorize]
        [HttpPatch("{id}/add-reviews")]
        public async Task<IActionResult> AddReviewById(int id, [FromBody] AddReviewDto dto)
        {
            var result = await _service.AddReviewsByIdAsync(id, dto);
            return Ok(result);
        }

        //[Authorize]
        [HttpPatch("{id}/delete-reviews/{email}")]
        public async Task<IActionResult> DeleteReviewById(int id, string email)
        {
            var result = await _service.RemoveReviewsByIdAsync(id, email);
            return Ok(result);
        }
    }
}
