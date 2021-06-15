using System.Collections.Generic;
using System.Threading.Tasks;
using WhereBNB.API.Controllers.Parameters;
using WhereBNB.API.Model;

namespace WhereBNB.API.Repositories
{
    public interface IListingRepository : IRepository<Listing>
    {
        Task<IEnumerable<Listing>> Get(ListingParameters p);
        Task<IEnumerable<ListingGeoData>> GetGeoData(ListingParameters p);
        Task<int> Count(ListingParameters p);
    }
}