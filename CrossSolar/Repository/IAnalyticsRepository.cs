using CrossSolar.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossSolar.Repository
{
    public interface IAnalyticsRepository : IGenericRepository<OneHourElectricity>
    {
        Task<IEnumerable<OneHourElectricity>> GetDayAnalyticsAsync(int panelId);
        Task<IEnumerable<OneHourElectricity>> GetPanelAnalyticsAsync(int panelId);
    }
}