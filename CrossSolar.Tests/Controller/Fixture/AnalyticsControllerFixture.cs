using CrossSolar.Domain;
using CrossSolar.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrossSolar.Tests.Controller.Fixture
{
    public class AnalyticsControllerFixture
    {
      public IPanelRepository PanelRepository { get; }
      public IAnalyticsRepository AnalyticsRepository { get; }
      public Panel Panel { get; set; }

      public AnalyticsControllerFixture()
      {
         PanelRepository = new Mock<IPanelRepository>().Object;
         AnalyticsRepository = new Mock<IAnalyticsRepository>().Object;

         Panel panel = new Panel()
         {
            Brand = "Areva",
            Latitude = 12.345678,
            Longitude = 98.7655432,
            Serial = "AAAA1111BBBB2222"
         };

         panel.Id = PanelRepository.InsertAsync(panel).Result;
         Panel = panel;
      }
   }
}
