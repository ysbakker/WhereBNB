using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhereBNB.API.Repositories;

namespace WhereBNB.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly ICalendarRepository _calendarRepository;
        public StatsController(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        [HttpGet("stays/{id:int}")]
        public async Task<IActionResult> GetStays(int id)
        {
            return Ok(await _calendarRepository.GetMonthlyStays(id));
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("availability/{id:int}")]
        public async Task<IActionResult> GetAvailability(int id)
        {
            return Ok(await _calendarRepository.GetListingAvailability(id));
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("availability/{neighbourhood}")]
        public async Task<IActionResult> GetAvailability(string neighbourhood)
        {
            return Ok(await _calendarRepository.GetNeighbourhoodAvailability(neighbourhood));
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("prices/{neighbourhood}")]
        public async Task<IActionResult> GetAveragePrice(string neighbourhood)
        {
            try
            {
                return Ok(await _calendarRepository.GetAverageNeighbourhoodPrice(neighbourhood));
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Neighbourhood {neighbourhood} not found");
            }
        }
    }
}