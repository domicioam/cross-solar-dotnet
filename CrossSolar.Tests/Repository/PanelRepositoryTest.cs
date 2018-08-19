using CrossSolar.Domain;
using CrossSolar.Repository;
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
    public class PanelRepositoryTest
    {
        [Fact]
        public async void InsertAsync_ShouldInsertPanel()
        {
            Panel panel = new Panel()
            {
                Brand = "Areva",
                Latitude = 12.345678,
                Longitude = 98.7655432,
                Serial = "AAAA1111BBBB2222"
            };

            var mockSet = new Mock<DbSet<Panel>>();

            var mockContext = new Mock<CrossSolarDbContext>();
            mockContext.Setup(m => m.Panels).Returns(mockSet.Object);
            mockContext.Setup(m => m.Set<Panel>()).Returns(mockSet.Object);

            var panelRepository = new PanelRepository(mockContext.Object);
            await panelRepository.InsertAsync(panel);
            mockSet.Verify(m => m.Add(It.IsAny<Panel>()), Times.Once());
        }
    }
}
