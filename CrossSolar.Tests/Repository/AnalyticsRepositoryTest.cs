using CrossSolar.Domain;
using CrossSolar.Repository;
using CrossSolar.Tests.Fixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CrossSolar.Tests.Repository
{
    public class AnalyticsRepositoryTest : IClassFixture<AnalyticsFixture>
    {
        public AnalyticsRepositoryTest(AnalyticsFixture fixture)
        {
            _fixture = fixture;
        }

        private AnalyticsFixture _fixture;

        [Fact]
        public async void GetDayAnalyticsAsync_ShouldReturnOnlyDayData()
        {
            int panelId = 1;
            var dayAnalytics = _fixture.MockOneHourEletricityData(panelId).AsQueryable();

            var mockSet = new Mock<DbSet<OneHourElectricity>>();
            mockSet.As<IQueryable<OneHourElectricity>>().Setup(m => m.Provider).Returns(dayAnalytics.Provider);
            mockSet.As<IQueryable<OneHourElectricity>>().Setup(m => m.Expression).Returns(dayAnalytics.Expression);
            mockSet.As<IQueryable<OneHourElectricity>>().Setup(m => m.ElementType).Returns(dayAnalytics.ElementType);
            mockSet.As<IQueryable<OneHourElectricity>>().Setup(m => m.GetEnumerator()).Returns(dayAnalytics.GetEnumerator());

            var mockContext = new Mock<CrossSolarDbContext>();
            mockContext.Setup(m => m.OneHourElectricitys).Returns(mockSet.Object);
            mockContext.Setup(m => m.Set<OneHourElectricity>()).Returns(mockSet.Object);

            var analyticsRepository = new AnalyticsRepository(mockContext.Object);

            var results = await analyticsRepository.GetDayAnalyticsAsync(panelId);

            Assert.All(results, result => Assert.Equal(DateTime.Now.Date, result.DateTime.Date));
        }
    }
}
