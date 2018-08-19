using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossSolar.Domain;

namespace CrossSolar.Repository
{
    public class AnalyticsRepository : GenericRepository<OneHourElectricity>, IAnalyticsRepository
    {
        public AnalyticsRepository(CrossSolarDbContext dbContext) : base(dbContext)
        { }

        public async Task<IEnumerable<OneHourElectricity>> GetDayAnalyticsAsync(int panelId)
        {
            var task = Task.Run(() =>
            {
                return Query().Where(o => o.PanelId == panelId && o.DateTime.Date == DateTime.Now.Date).AsEnumerable();
            });

            return await task;
        }

        public async Task<IEnumerable<OneHourElectricity>> GetPanelAnalyticsAsync(int panelId)
        {
            var task = Task.Run(()=> {
                return Query().Where(x => x.PanelId.Equals(panelId)).AsEnumerable();
            });

            return await task;
        }
    }
}