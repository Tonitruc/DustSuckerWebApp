using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.UserServices;

namespace DustSuckerWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;


        public UserController(UserService userService)
        {
            _userService = userService;    
        }


        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserInfo(string email)
        {
            var res = await _userService.GetUserInfoAsync(email);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersInfo()
        {
            var res = await _userService.GetUsersInfoAsync();
            return Ok(res);
        }

        //[Authorize]
        [HttpGet("cart-short/{email}")]
        public async Task<IActionResult> GetShortFavoriteAdvertisements(string email)
        {
            var res = await _userService.GetShortFavoriteAdvertisementByUserEmailAsync(email);
            return Ok(res);
        }

        //[Authorize]
        [HttpGet("cart/{email}")]
        public async Task<IActionResult> GetFavoriteAdvertisements(string email)
        {
            var res = await _userService.GetFavoriteAdvertisementByUserEmailAsync(email);
            return Ok(res);
        }


        //[Authorize]
        [HttpPatch("cart-add/{email}/{title}")]
        public async Task<IActionResult> AddFavoriteAdvertisementByUserEmail(string email, string title)
        {
            var res = await _userService.AddFavoriteAdvertisementByUserEmailAsync(email, title);
            return Ok(res);
        }

       // [Authorize]
        [HttpPatch("cart-remove/{email}/{title}")]
        public async Task<IActionResult> DeleteFavoriteAdvertisementByUserEmail(string email, string title)
        {
            var res = await _userService.DeleteFavoriteAdvertisementByUserEmailAsync(email, title);
            return Ok(res);
        }

        //[Authorize]
        [HttpPatch("cart-add/{email}")]
        public async Task<IActionResult> AddFavoriteAdvertisementsByUserEmail(string email,
            [FromBody] List<string> titles)
        {
            var res = await _userService.AddFavoriteAdvertisementsByUserEmailAsync(email, titles);
            return Ok(res);
        }

        //[Authorize]
        [HttpPatch("cart-remove/{email}")]
        public async Task<IActionResult> DeleteFavoriteAdvertisementsByUserEmail(string email,
            [FromBody] List<string> titles)
        {
            var res = await _userService.DeleteFavoriteAdvertisementsByUserEmailAsync(email, titles);
            return Ok(res);
        }
    }
}
