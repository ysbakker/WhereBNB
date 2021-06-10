using System.Threading.Tasks;
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

        [HttpGet]
        [Route("stays/{id:int}")]
        public async Task<IActionResult> GetStays(int id)
        {
            return Ok(await _calendarRepository.GetMonthlyStays(id));
        }
    }
}