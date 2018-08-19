using CrossSolar.Domain;
using CrossSolar.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CrossSolar.Tests.Repository
{
    public class PanelRepositoryTest
    {
      public PanelRepositoryTest()
      {
         _panelRepository = _panelRepositoryMock.Object;
      }

      private readonly Mock<IPanelRepository> _panelRepositoryMock = new Mock<IPanelRepository>();
      private readonly IPanelRepository _panelRepository;


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

         await _panelRepository.InsertAsync(panel);
         var insertedPanel = await _panelRepository.GetAsync(panel.Id.ToString());
         Assert.NotNull(insertedPanel);
      }
   }
}
