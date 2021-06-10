using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhereBNB.API.Model;

namespace WhereBNB.API.Repositories
{
    public class CalendarRepository : Repository<Calendar>, ICalendarRepository
    {
        private readonly DbSet<Listing> _listingTable;
        public CalendarRepository(WhereBNBContext context) : base(context)
        {
            _listingTable = context.Set<Listing>();
        }

        public async Task<int> GetMonthlyStays(int id)
        {
            return await _listingTable
                .Where(l => l.Id == id)
                .Join(Table, listing => listing.Id, calendar => calendar.ListingId, (listing, calendar) => calendar)
                .Where(e => e.Available == "f")
                .CountAsync();
        }
    }
}