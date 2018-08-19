using CrossSolar.Domain;

namespace CrossSolar.Repository
{
    public class AnalyticsRepository : GenericRepository<OneHourElectricity>, IAnalyticsRepository
    {
        public AnalyticsRepository(CrossSolarDbContext dbContext) : base(dbContext)
        { }
    }
}