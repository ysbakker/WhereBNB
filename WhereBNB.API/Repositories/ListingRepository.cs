using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhereBNB.API.Controllers.Parameters;
using WhereBNB.API.Model;

namespace WhereBNB.API.Repositories
{

    public class ListingRepository : Repository<Listing>, IListingRepository
    {
        public ListingRepository(WhereBNBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Listing>> Get(ListingParameters p)
        {
            return await CreateQuery(p).OrderBy(l => l.Name).ToListAsync();
        }

        public async Task<int> Count(ListingParameters p)
        {
            return await CreateQuery(p).OrderBy(l => l.Name).CountAsync();
        }

        private IQueryable<Listing> CreateQuery(ListingParameters p)
        {
            var query = Table.AsQueryable();
            if (!string.IsNullOrEmpty(p.Neighbourhood))
            {
                query = query.Where(l => l.NeighbourhoodCleansed == p.Neighbourhood);
            }

            if (p.PriceFrom.HasValue && p.PriceTo.HasValue)
            {
                query = query.Where(l => p.PriceFrom.Value <= l.Price && l.Price <= p.PriceTo.Value);
            }

            if (p.ReviewFrom.HasValue && p.ReviewTo.HasValue)
            {
                query = query.Where(l =>
                    p.ReviewFrom.Value <= l.ReviewScoresRating && l.ReviewScoresRating <= p.ReviewTo.Value);
            }

            if (p.Page.HasValue && p.PageSize.HasValue)
            {
                int skip = p.Page.Value * p.PageSize.Value - p.PageSize.Value;
                query = query.Skip(skip).Take(p.PageSize.Value);
            }

            return query;
        }
    }
}