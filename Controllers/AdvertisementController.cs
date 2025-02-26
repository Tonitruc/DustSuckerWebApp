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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hoover = await _service.GetByIdAsync(id);
            if (hoover == null)
            {
                return BadRequest("Invalid Id.");
            }

            return Ok(hoover);
        }

        [HttpGet("{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var hoover = await _service.GetByTitleAsync(title);
            if (hoover == null)
            {
                return BadRequest("Invalid Id.");
            }

            return Ok(hoover);
        }

        [HttpGet("short")]
        public async Task<IActionResult> GetShort()
        {
            var hooversList = await _service.GetShortAsync();

            return Ok(hooversList);
        }


        [HttpGet("short/{id:int}")]
        public async Task<IActionResult> GetShortById(int id)
        {
            var hoover = await _service.GetShortByIdAsync(id);
            if (hoover == null)
            {
                return BadRequest("Invalid Id.");
            }

            return Ok(hoover);
        }

        [HttpGet("short/{title}")]
        public async Task<IActionResult> GetShortByTitle(string title)
        {
            var hoover = await _service.GetShortByTitleAsync(title);
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

        [HttpPatch("add-image/{id:int}")]
        public async Task<IActionResult> AddImageUrl(int id, [FromQuery] string imageUrl)
        {
            var res = await _service.AddImageUrlByIdAsync(id, imageUrl);
            if(res == null)
                return BadRequest("Invalid advertisement id");

            return Ok(res);
        }

        [HttpPatch("remove-image/{id:int}")]
        public async Task<IActionResult> RemoveImageUrl(int id, [FromQuery] string imageUrl)
        {
            var res = await _service.RemoveImageUrlByIdAsync(id, imageUrl);
            if (res == null)
                return BadRequest("Invalid advertisement id");

            return Ok(res);
        }

        [HttpPatch("add-image/{title}")]
        public async Task<IActionResult> AddImageUrl(string title, [FromQuery] string imageUrl)
        {
            var res = await _service.AddImageUrlByTitleAsync(title, imageUrl);
            if (res == null)
                return BadRequest("Invalid advertisement id");

            return Ok(res);
        }

        [HttpPatch("add-images/{id}")]
        public async Task<IActionResult> AddImageUrl(int id, [FromBody] List<string> imagesUrls)
        {
            var res = await _service.AddImagesUrlByIdAsync(id, imagesUrls);
            if (res == null)
                return BadRequest("Invalid advertisement id");

            return Ok(res);
        }

        [HttpPatch("set-main_image/{id:int}")]
        public async Task<IActionResult> SetAsTitleImageById(int id, string imagesUrl)
        {
            var res = await _service.SetAsMainImageByIdAsync(id, imagesUrl);
            if (res == null)
                return BadRequest("Invalid advertisement id");

            return Ok(res);
        }

        [HttpPatch("set-main_image/{title}")]
        public async Task<IActionResult> SetAsTitleImageByTitle(string title, string imagesUrl)
        {
            var res = await _service.SetAsMainImageByTitleAsync(title, imagesUrl);
            if (res == null)
                return BadRequest("Invalid advertisement id");

            return Ok(res);
        }


        [HttpPatch("remove-image/{title}")]
        public async Task<IActionResult> RemoveImageUrl(string title, [FromQuery] string imageUrl)
        {
            var res = await _service.RemoveImageUrlByTitleAsync(title, imageUrl);
            if (res == null)
                return BadRequest("Invalid advertisement id");

            return Ok(res);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteByIdAsync(id);

            if(result)
                return Ok(result);

            return BadRequest("Invalid id");
        }

        [HttpDelete("{title}")]
        public async Task<IActionResult> Delete(string title)
        {
            var result = await _service.DeleteByTitleAsync(title);

            if (result)
                return Ok(result);

            return BadRequest("Invalid id");
        }

    }
}
