using Foxy.DataLayer.Models.Games;
using System.Net.Http.Json;
using FluentAssertions;

namespace ApiIntegrationTest;

public class GameCategoryControllerTests
{

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // Arrange
        var application = new FoxyWebApiFactory();
        var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/api/GameCategory");

        // Assert
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<GameCategory[]>();
        res.Length.Should().BeInRange(0, 0);
    }
}