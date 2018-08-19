using Xunit;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using CrossSolar.Models;
using CrossSolar.Controllers;
using CrossSolar.Tests.Fixture;
using Microsoft.AspNetCore.Mvc;

namespace CrossSolar.Tests.Controller
{
    public class AnalyticsControllerTest : IClassFixture<AnalyticsFixture>
    {
        public AnalyticsControllerTest(AnalyticsFixture fixture)
        {
            _fixture = fixture;
            _analyticsController = new AnalyticsController(fixture.AnalyticsRepository, fixture.PanelRepository);
        }

        private readonly AnalyticsFixture _fixture;
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
            var result = await _analyticsController.Post(_fixture.Panel.Id, oneHourElectricity);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async Task DayResults_ShouldReturnDayAnalytics()
        {
            // Arrange
            int panelId = _fixture.Panel.Id;

            // Act
            var result = await _analyticsController.DayResults(panelId);

            // Assert
            Assert.NotNull(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Get_ShouldReturnPanelAnalytics()
        {
            // Arrange
            int panelId = _fixture.Panel.Id;

            // Act
            var result = await _analyticsController.Get(panelId);

            // Assert
            Assert.NotNull(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
