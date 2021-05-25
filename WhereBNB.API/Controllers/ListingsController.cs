using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereBNB.API.Model;
using WhereBNB.API.Repositories;

namespace WhereBNB.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingsController : ControllerBase
    {
        private IRepository<Listing> ListingRepository { get; set; }
        private IRepository<SummaryListing> SummaryListingRepository { get; set; }
        
        public ListingsController(IRepository<Listing> listingRepository, IRepository<SummaryListing> summaryListingRepository)
        {
            ListingRepository = listingRepository;
            SummaryListingRepository = summaryListingRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var listings = await SummaryListingRepository.GetAll();
            return Ok(listings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var listing = await ListingRepository.GetById(id);
            if (listing == null) return NotFound();
            return Ok(listing);
        }
    }
}