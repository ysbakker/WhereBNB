using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereBNB.API.Model;
using WhereBNB.API.Repositories;

namespace WhereBNB.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NeighbourhoodsController : ControllerBase
    {
        private IRepository<Neighbourhood> NeighbourhoodRepository { get; }
        public NeighbourhoodsController(IRepository<Neighbourhood> neighbourhoodRepository)
        {
            NeighbourhoodRepository = neighbourhoodRepository;
        }

        public async Task<IActionResult> Get()
        {
            var neighbourhoods = await NeighbourhoodRepository.GetAll();
            return Ok(neighbourhoods.Select(n => n.Neighbourhood1).ToList());
        }
    }
}