using Xunit;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using CrossSolar.Models;
using CrossSolar.Controllers;
using CrossSolar.Tests.Controller.Fixture;
using Microsoft.AspNetCore.Mvc;

namespace CrossSolar.Tests.Controller
{
    public class AnalyticsControllerTest : IClassFixture<AnalyticsControllerFixture>
    {
        public AnalyticsControllerTest(AnalyticsControllerFixture fixture)
        {
            _fixture = fixture;
            _analyticsController = new AnalyticsController(fixture.AnalyticsRepository, fixture.PanelRepository);
        }

        private readonly AnalyticsControllerFixture _fixture;
        private readonly AnalyticsController _analyticsController;

        [Fact]
        public async Task Post_ShouldInsertOneHourElectricity()
        {
            // Arrange
            var oneHourElectricity = new OneHourElectricityModel
            {
                KiloWatt = 10,
                DateTime = DateTime.Now
            };

            // Act
            var result = await _analyticsController.Post(_fixture.Panel.Id.ToString(), oneHourElectricity);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }
    }
}
