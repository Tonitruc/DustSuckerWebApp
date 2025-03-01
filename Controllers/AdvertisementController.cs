using DustSuckerWebApp.ServiceLayer.AdvertisementsServices;
using DustSuckerWebApp.ServiceLayer.AdvertisementsServices.QueryObject;
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

        /// <summary>
        /// Get list of advertisement with filter, sort and pagination.
        /// </summary>
        /// <param name="pageNum">Page number, if you need all the data, do not specify.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="sortedBy">Sort: costAscending, costDescending, rating, publishDate. Or as in the database</param>
        /// <param name="queries">Filter params.</param>
        /// <returns>List of advertisement.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AdvertisementDto>))]
        public async Task<IActionResult> Get(int? pageNum, int? pageSize, 
            string? sortedBy, 
            [FromQuery] AdvertisementFilterParameters queries)
        { 
            var hooversList = await _service.GetAsync(pageNum, pageSize, sortedBy, queries);

            return Ok(hooversList);
        }

        /// <summary>
        /// Get advertisement by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdvertisementDto))]
        public async Task<IActionResult> GetById(int id)
        {
            var hoover = await _service.GetByIdAsync(id);
            if (hoover == null)
            {
                return BadRequest("Invalid Id.");
            }

            return Ok(hoover);
        }

        /// <summary>
        /// Get advertisement by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet("{title}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdvertisementDto))]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var hoover = await _service.GetByTitleAsync(title);
            if (hoover == null)
            {
                return BadRequest("Invalid Id.");
            }

            return Ok(hoover);
        }

        /// <summary>
        /// Get list of advertisement with filter, sort and pagination.
        /// </summary>
        /// <param name="pageNum">Page number, if you need all the data, do not specify.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="sortedBy">Sort: costAscending, costDescending, rating, publishDate. Or as in the database</param>
        /// <param name="queries">Filter params.</param>
        /// <returns>List of advertisement.</returns>
        [HttpGet("short")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AdvertisementShortDto>))]
        public async Task<IActionResult> GetShort(int? pageNum, int? pageSize,
            string? sortedBy,
            [FromQuery] AdvertisementFilterParameters queries)
        {
            var hooversList = await _service.GetShortAsync(pageNum, pageSize, sortedBy, queries);

            return Ok(hooversList);
        }

        /// <summary>
        /// Get short info about advertisement by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("short/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdvertisementShortDto))]
        public async Task<IActionResult> GetShortById(int id)
        {
            var hoover = await _service.GetShortByIdAsync(id);
            if (hoover == null)
            {
                return BadRequest("Invalid Id.");
            }

            return Ok(hoover);
        }

        /// <summary>
        /// Get short info about advertisement by id
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet("short/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdvertisementShortDto))]             
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

        /// <summary>
        /// Set main image from list of added images for advertisement
        /// </summary>
        /// <param name="title">Title of advertisement</param>
        /// <param name="imagesUrl">Exist image in list of images</param>
        /// <returns></returns>
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
