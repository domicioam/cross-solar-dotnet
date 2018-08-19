using CrossSolar.Domain;

namespace CrossSolar.Repository
{
    public class PanelRepository : GenericRepository<Panel>, IPanelRepository
    {
        public PanelRepository(CrossSolarDbContext dbContext) : base(dbContext)
        { }
    }
}