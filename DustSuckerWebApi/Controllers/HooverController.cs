﻿using ServiceLayer.HoverServices;
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
            var hooversList = await _service.GetHoovers();

            return Ok(hooversList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hoover = await _service.GetHoover(id);
            if(hoover == null)
            {
                return BadRequest("Invalid Id.");
            }

            return Ok(hoover);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddHooverDto dto)
        {
            try
            {
                var result = await _service.AddHoover(dto);
                return Ok(result);
            }
            catch(ValidationException ex)
            {
                return BadRequest(new { Message = ex.Message, 
                                        Errors = ex.ValidationResult });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _service.Remove(id);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
