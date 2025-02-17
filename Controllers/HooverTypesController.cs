using DustSuckerWebApp.Extensions;
using DustSuckerWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DustSuckerWebApp.Controllers
{
    [ApiController]
    [Route("api/hoover-types")]
    public class HooverTypesController : ControllerBase
    {
        [HttpGet]
        public IActionResult BaseHooverTypes()
        {
            return Ok(new List<object>()
            {
                new { Category = nameof(HooverAttributes.HooverTypes), Values = HooverAttributes.HooverTypes },
                new { Category = nameof(HooverAttributes.DustBagTypes), Values = HooverAttributes.DustBagTypes },
                new { Category = nameof(HooverAttributes.FilterTypes), Values = HooverAttributes.FilterTypes },
                new { Category = nameof(HooverAttributes.PowerTypes), Values = HooverAttributes.PowerTypes },
                new { Category = nameof(HooverAttributes.TubeTypes), Values = HooverAttributes.TubeTypes },
                new { Category = nameof(HooverAttributes.CleaningTypes), Values = HooverAttributes.CleaningTypes },
                new { Category = nameof(HooverAttributes.NozzleTypes), Values = HooverAttributes.NozzleTypes },
            });
        }
    }
}
