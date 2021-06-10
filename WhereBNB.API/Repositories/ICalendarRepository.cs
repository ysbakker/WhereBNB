using System.Threading.Tasks;
using WhereBNB.API.Model;

namespace WhereBNB.API.Repositories
{
    public interface ICalendarRepository : IRepository<Calendar>
    {
        Task<int> GetMonthlyStays(int id);
    }
}