using Foxy.DataLayer.Models.Games;
using System.Net.Http.Json;
using FluentAssertions;
using System.Net;
using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using static System.Net.Mime.MediaTypeNames;

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
        //res.Length.Should().BeInRange(0, 0);
        res.Should().NotBeNull();

    }

    [Fact]
    public async Task GetById_NotFound_Returns404()
    {
        var application = new FoxyWebApiFactory();
        var client = application.CreateClient();
        var response = await client.GetAsync($"/api/GameCategory/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_And_GetById_Works()
    {
        //AddFakeUser();
        var application = new FoxyWebApiFactory();
        var client = application.CreateClient();
        var req = new GameCategoryReqDto { Title = "TestCategory" };
        var postResponse = await client.PostAsJsonAsync("/api/GameCategory", req);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<GameCategory>();
        Assert.NotNull(created);
        Assert.Equal("TestCategory", created.Title);

        var getResponse = await client.GetAsync($"/api/GameCategory/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var getResult = await getResponse.Content.ReadFromJsonAsync<GameCategoryResDto>();
        Assert.NotNull(getResult);
        Assert.Equal("TestCategory", getResult.Title);
    }

   
    [Fact]
    public async Task Update_Works_And_BadRequest_On_Id_Mismatch()
    {
        var application = new FoxyWebApiFactory();
        var client = application.CreateClient();
        // Create first
        var req = new GameCategoryReqDto { Title = "ToUpdate" };
        var postResponse = await client.PostAsJsonAsync("/api/GameCategory", req);
        var created = await postResponse.Content.ReadFromJsonAsync<GameCategory>();

        // Update with correct id
        var updateReq = new GameCategoryReqDto { Id = created.Id, Title = "Updated" };
        var putResponse = await client.PutAsJsonAsync($"/api/GameCategory/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        // Update with mismatched id
        var badUpdateReq = new GameCategoryReqDto { Id = Guid.NewGuid(), Title = "Bad" };
        var badPutResponse = await client.PutAsJsonAsync($"/api/GameCategory/{created.Id}", badUpdateReq);
        Assert.Equal(HttpStatusCode.BadRequest, badPutResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_Works_And_NotFound()
    {
        var application = new FoxyWebApiFactory();
        var client = application.CreateClient();
        // Create first
        var req = new GameCategoryReqDto { Title = "ToDelete" };
        var postResponse = await client.PostAsJsonAsync("/api/GameCategory", req);
        var created = await postResponse.Content.ReadFromJsonAsync<GameCategory>();

        // Delete
        var delResponse = await client.DeleteAsync($"/api/GameCategory/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delResponse.StatusCode);

        // Delete again (should be not found)
        var delResponse2 = await client.DeleteAsync($"/api/GameCategory/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, delResponse2.StatusCode);
    }

}