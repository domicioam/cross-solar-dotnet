using CrossSolar.Domain;
using CrossSolar.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossSolar.Tests.Fixture
{
    public class AnalyticsFixture
    {
        public IPanelRepository PanelRepository { get; }
        public IAnalyticsRepository AnalyticsRepository { get; }
        public Panel Panel { get; set; }

        public AnalyticsFixture()
        {
            string panelId = "1";
            Panel panel = new Panel()
            {
                Id = Int32.Parse(panelId),
                Brand = "Areva",
                Latitude = 12.345678,
                Longitude = 98.7655432,
                Serial = "AAAA1111BBBB2222"
            };

            List<OneHourElectricity> oneHourElectricities = MockOneHourEletricityData(panelId);
            var dayAnalytics = oneHourElectricities.Where(o => o.DateTime.Date == DateTime.Now.Date);

            //Repositories setup
            var panelRepositoryMock = new Mock<IPanelRepository>();
            panelRepositoryMock.Setup(m => m.GetAsync(panelId)).ReturnsAsync(panel).Verifiable();

            var analyticsRepositoryMock = new Mock<IAnalyticsRepository>();
            analyticsRepositoryMock.Setup(m => m.GetDayAnalyticsAsync(panelId)).ReturnsAsync(dayAnalytics).Verifiable();

            //Fill public properties
            PanelRepository = panelRepositoryMock.Object;
            AnalyticsRepository = analyticsRepositoryMock.Object;
            Panel = panel;
        }

        public List<OneHourElectricity> MockOneHourEletricityData(string panelId)
        {
            List<OneHourElectricity> oneHourElectricities = new List<OneHourElectricity>();
            Random random = new Random();

            //Mock data from today to 10 days ago
            for (int day = 0; day <= 10; day++)
            {
                for (int hour = 0; hour < 10; hour++)
                {
                    var oneHourElectricity = new OneHourElectricity
                    {
                        PanelId = panelId,
                        KiloWatt = random.Next(100, 1000),
                        DateTime = DateTime.Now.AddDays(-day).AddHours(hour)
                    };

                    oneHourElectricities.Add(oneHourElectricity);
                }
            }

            return oneHourElectricities;
        }
    }
}
