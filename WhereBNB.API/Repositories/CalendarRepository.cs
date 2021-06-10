using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WhereBNB.API.Model;

namespace WhereBNB.API.Repositories
{
    public class CalendarRepository : Repository<Calendar>, ICalendarRepository
    {
        private readonly DbSet<Listing> _listingTable;
        private readonly WhereBNBContext _context;
        public CalendarRepository(WhereBNBContext context) : base(context)
        {
            _context = context;
            _listingTable = context.Set<Listing>();
        }

        public async Task<int> GetMonthlyStays(int id)
        {
            return await Table
                .Where(c => c.ListingId == id)
                .Where(c => c.Available == "f")
                .CountAsync();
        }

        public async Task<IEnumerable<Availability>> GetListingAvailability(int id)
        {
            return await Table
                .Where(c => c.ListingId == id)
                .GroupBy(c => c.Date.Month)
                .Select(c => new Availability
                {
                    Month = c.Key,
                    Available = c.Count(e => e.Date.Month == c.Key && e.Available == "t"),
                    Total = c.Count(e => e.Date.Month == c.Key),
                })
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Availability>> GetNeighbourhoodAvailability(string neighbourhood)
        {
            return await Table
                .Where(c => c.Listing.NeighbourhoodCleansed == neighbourhood)
                .GroupBy(c => c.Date.Month)
                .Select(c => new Availability
                {
                    Month = c.Key,
                    Available = c.Count(e => e.Date.Month == c.Key && e.Available == "t"),
                    Total = c.Count(e => e.Date.Month == c.Key),
                })
                .ToListAsync();
        }
        
        public async Task<double> GetAverageNeighbourhoodPrice(string neighbourhood)
        {
            return await Table
                .Where(c => c.Listing.NeighbourhoodCleansed == neighbourhood)
                .Select(c => c.Listing.Price)
                .AverageAsync();
        }
    }
}