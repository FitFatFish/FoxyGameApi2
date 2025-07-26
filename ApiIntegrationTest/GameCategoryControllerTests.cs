using Foxy.DataLayer.Models.Games;
using System.Net.Http.Json;
using FluentAssertions;
using System.Net;
using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using Foxy.DataLayer.Models.Users;

namespace ApiIntegrationTest;




public class GameCategoryControllerTests
{
    private readonly FoxyWebApiFactory _application;
    private readonly HttpClient _client;
    private UserProfile _userprofile;

    public GameCategoryControllerTests()
    {
        _application = new FoxyWebApiFactory();
        _client = _application.CreateClient();
        InitializeCategoriesAsync().GetAwaiter().GetResult();
    }

    private async Task InitializeCategoriesAsync()
    {
        // Check if userpfiles already exist to avoid duplicates
        var responseuser = await _client.GetAsync("/api/UserProfile");
        var userprofiles = await responseuser.Content.ReadFromJsonAsync<UserProfile[]>();
        if (userprofiles != null && userprofiles.Length >= 1)
        {
            _userprofile = userprofiles[0];

            return;
        }
        else
        {
            //create first user
            var requser = new UserProfileReqDto
            {
                AccountId = "@user1",
                Bio = "string",
                HeaderImageGuid = "string",
                ProfileImageGuid = "string"
            };
            var postResponseuser = await _client.PostAsJsonAsync("/api/UserProfile", requser);
            postResponseuser.EnsureSuccessStatusCode();
            _userprofile = await postResponseuser.Content.ReadFromJsonAsync<UserProfile>();
        }

    }
    [Fact]
    public async Task GetAll_ReturnsOk()
    {
       
        // Act
        var response = await _client.GetAsync("/api/GameCategory");

        // Assert
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<GameCategory[]>();
        //res.Length.Should().BeInRange(0, 0);
        res.Should().NotBeNull();
    }

    [Fact]
    public async Task GetById_NotFound_Returns404()
    {
       
        
        var response = await _client.GetAsync($"/api/GameCategory/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_And_GetById_Works()
    {
        //AddFakeUser();
       
        
        var req = new GameCategoryReqDto { Title = "TestCategory",CreatedBy=_userprofile.Id };
        var postResponse = await _client.PostAsJsonAsync("/api/GameCategory", req);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<GameCategory>();
        Assert.NotNull(created);
        Assert.Equal("TestCategory", created.Title);

        var getResponse = await _client.GetAsync($"/api/GameCategory/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var getResult = await getResponse.Content.ReadFromJsonAsync<GameCategoryResDto>();
        Assert.NotNull(getResult);
        Assert.Equal("TestCategory", getResult.Title);
    }


    [Fact]
    public async Task Update_Works_And_BadRequest_On_Id_Mismatch()
    {
       
        
        // Create first
        var req = new GameCategoryReqDto { Title = "ToUpdate",CreatedBy = _userprofile.Id };
        var postResponse = await _client.PostAsJsonAsync("/api/GameCategory", req);
        var created = await postResponse.Content.ReadFromJsonAsync<GameCategory>();

        // Update with correct id
        var updateReq = new GameCategoryReqDto { Id = created.Id, Title = "Updated" };
        var putResponse = await _client.PutAsJsonAsync($"/api/GameCategory/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        // Update with mismatched id
        var badUpdateReq = new GameCategoryReqDto { Id = Guid.NewGuid(), Title = "Bad" };
        var badPutResponse = await _client.PutAsJsonAsync($"/api/GameCategory/{created.Id}", badUpdateReq);
        Assert.Equal(HttpStatusCode.BadRequest, badPutResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_Works_And_NotFound()
    {
       
        
        // Create first
        var req = new GameCategoryReqDto { Title = "ToDelete",CreatedBy= _userprofile.Id };
        var postResponse = await _client.PostAsJsonAsync("/api/GameCategory", req);
        var created = await postResponse.Content.ReadFromJsonAsync<GameCategory>();

        // Delete
        var delResponse = await _client.DeleteAsync($"/api/GameCategory/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delResponse.StatusCode);

        // Delete again (should be not found)
        var delResponse2 = await _client.DeleteAsync($"/api/GameCategory/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, delResponse2.StatusCode);
    }

}