using System.Collections.Generic;
using System.Threading.Tasks;
using WhereBNB.API.Model;

namespace WhereBNB.API.Repositories
{
    public interface ICalendarRepository : IRepository<Calendar>
    {
        Task<int> GetMonthlyStays(int id);
        Task<IEnumerable<Availability>> GetListingAvailability(int id);
        Task<IEnumerable<Availability>> GetNeighbourhoodAvailability(string neighbourhood);
        Task<double> GetAverageNeighbourhoodPrice(string neighbourhood);
    }
}