using ServiceLayer.AdvertisementsServices;
using ViewModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DustSuckerWebApi.Controllers
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
            return Ok(hoover);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddAdvertisementDto dto)
        {
            var result = await _service.AddAsync(dto);
            return Ok(result);
        }

        //[Authorize]
        [HttpPatch("add-image/{id:int}")]
        public async Task<IActionResult> AddImageUrl(int id, [FromQuery] string imageUrl)
        {
            var res = await _service.AddImageUrlByIdAsync(id, imageUrl);
            return Ok(res);
        }

        //[Authorize]
        [HttpPatch("remove-image/{id:int}")]
        public async Task<IActionResult> DeleteImageUrl(int id, [FromQuery] string imageUrl)
        {
            var res = await _service.DeleteImageUrlByIdAsync(id, imageUrl);
            return Ok(res);
        }

        //[Authorize]
        [HttpPatch("add-image/{title}")]
        public async Task<IActionResult> AddImageUrl(string title, [FromQuery] string imageUrl)
        {
            var res = await _service.AddImageUrlByTitleAsync(title, imageUrl);
            return Ok(res);
        }

        //[Authorize]
        [HttpPatch("add-images/{id}")]
        public async Task<IActionResult> AddImageUrl(int id, [FromBody] List<string> imagesUrls)
        {
            var res = await _service.AddImagesUrlByIdAsync(id, imagesUrls);
            return Ok(res);
        }

        //[Authorize]
        [HttpPatch("set-main_image/{id:int}")]
        public async Task<IActionResult> SetAsTitleImageById(int id, string imagesUrl)
        {
            var res = await _service.SetAsMainImageByIdAsync(id, imagesUrl);
            return Ok(res);
        }

        /// <summary>
        /// Set main image from list of added images for advertisement
        /// </summary>
        /// <param name="title">Title of advertisement</param>
        /// <param name="imagesUrl">Exist image in list of images</param>
        /// <returns></returns>
        //[Authorize]
        [HttpPatch("set-main_image/{title}")]
        public async Task<IActionResult> SetAsTitleImageByTitle(string title, string imagesUrl)
        {
            var res = await _service.SetAsMainImageByTitleAsync(title, imagesUrl);
            return Ok(res);
        }

        //[Authorize]
        [HttpPatch("remove-image/{title}")]
        public async Task<IActionResult> DeleteImageUrl(string title, [FromQuery] string imageUrl)
        {
            var res = await _service.DeleteImageUrlByTitleAsync(title, imageUrl);
            return Ok(res);
        }

        //[Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _service.DeleteByIdAsync(id);
            return BadRequest("Invalid id");
        }

        //[Authorize]
        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteByTitle(string title)
        {
            var result = await _service.DeleteByTitleAsync(title);
            return BadRequest("Invalid id");
        }

    }
}
