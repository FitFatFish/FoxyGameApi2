
using FluentAssertions;
using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using Foxy.DataLayer.Models.Games;
using System.Net;
using System.Net.Http.Json;

namespace ApiIntegrationTest;
[TestCaseOrderer("ApiIntegrationTest.TestExecutionOrderer", "ApiIntegrationTest")]
public class MatchControllerTests
{
    private readonly FoxyWebApiFactory _application;
    private readonly HttpClient _client;
    private GameResDto _game1;
    private GameResDto _game2;
    private GameCategoryResDto _category1;
    private UserProfileResDto _userprofile;

    public MatchControllerTests()
    {
        _application = new FoxyWebApiFactory();
        _client = _application.CreateClient();
        InitializeMachesAsync().GetAwaiter().GetResult();
    }

    private async Task InitializeMachesAsync()
    {
        // Check if games already exist to avoid duplicates
        var response = await _client.GetAsync("/api/Game");
        var games = await response.Content.ReadFromJsonAsync<GameResDto[]>();
        if (games != null && games.Length >= 2)
        {
            _game1 = games[0];
            _game2 = games[1];

            return;
        }
        else
        {// Check if userpfiles already exist to avoid duplicates
            var responseuser = await _client.GetAsync("/api/UserProfile");
            var userprofiles = await responseuser.Content.ReadFromJsonAsync<UserProfileResDto[]>();
            if (userprofiles != null && userprofiles.Length >= 1)
            {
                _userprofile = userprofiles.First();


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
                _userprofile = await postResponseuser.Content.ReadFromJsonAsync<UserProfileResDto>();
            }
            //check gamecategory already exist
            var responsegamecategory = await _client.GetAsync("/api/GameCategory");
            var categories = await responsegamecategory.Content.ReadFromJsonAsync<GameCategoryResDto[]>();
            if (categories != null && categories.Length >= 2)
            {
                _category1 = categories.First();


            }
            else
            {
                // Create first category
                var req1 = new GameCategoryReqDto { Title = "Category1", CreatedBy = _userprofile.Id };
                var postResponse1 = await _client.PostAsJsonAsync("/api/GameCategory", req1);
                postResponse1.EnsureSuccessStatusCode();
                _category1 = await postResponse1.Content.ReadFromJsonAsync<GameCategoryResDto>();


            }
            //create first game
            var req = new GameReqDto
            {
                Title = "TestGame",
                GameCategoryId = _category1.Id,
                CreatedBy = _userprofile.Id,
                Documentation = "test",
                ImageGuid = "test"
            };
            var postResponse = await _client.PostAsJsonAsync("/api/Game", req);
            postResponse.EnsureSuccessStatusCode();
            _game1 = await postResponse.Content.ReadFromJsonAsync<GameResDto>();

            var reqgame2 = new GameReqDto
            {
                Title = "TestGame2",
                GameCategoryId = _category1.Id,
                CreatedBy = _userprofile.Id,
                Documentation = "test",
                ImageGuid = "test"
            };
            var postResponseGame2 = await _client.PostAsJsonAsync("/api/Game", reqgame2);
            postResponseGame2.EnsureSuccessStatusCode();
            _game2 = await postResponseGame2.Content.ReadFromJsonAsync<GameResDto>();
        }

    }


    [Fact, TestPriority(1)]
    public async Task GetAll_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/Match");

        // Assert
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<MatchResDto[]>();
        //res.Length.Should().BeInRange(0, 0);
        res.Should().NotBeNull();

    }

    [Fact, TestPriority(2)]
    public async Task GetById_NotFound_Returns404()
    {


        var response = await _client.GetAsync($"/api/Match/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact, TestPriority(3)]
    public async Task Create_And_GetById_Works()
    {
        //AddFakeUser();


        var req = new Match
        {
            GameId = _game1.Id,
            BeginDate = DateTime.UtcNow,

        };
        var postResponse = await _client.PostAsJsonAsync("/api/Match", req);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<MatchResDto>();
        Assert.NotNull(created);
        Assert.Equal(req.GameId, created.GameId);

        var getResponse = await _client.GetAsync($"/api/Match/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var getResult = await getResponse.Content.ReadFromJsonAsync<MatchResDto>();
        Assert.NotNull(getResult);
        Assert.Equal(req.GameId, getResult.GameId);
    }

    [Fact, TestPriority(4)]
    public async Task Update_Works_And_BadRequest_On_Id_Mismatch()
    {
        // Create first
        var req = new MatchReqDto
        {
            GameId = _game1.Id,
            BeginDate = DateTime.UtcNow,

        };
        var postResponse = await _client.PostAsJsonAsync("/api/Match", req);
        var created = await postResponse.Content.ReadFromJsonAsync<MatchResDto>();

        // Update with correct id
        var updateReq = new MatchReqDto
        {
            Id = created.Id,
            GameId = _game2.Id,
            BeginDate = DateTime.UtcNow,

        };
        var putResponse = await _client.PutAsJsonAsync($"/api/Match/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        // Update with mismatched id
        var badUpdateReq = new Match
        {
            Id = Guid.NewGuid(),
            GameId = _game1.Id,
            BeginDate = DateTime.UtcNow,

        };
        var badPutResponse = await _client.PutAsJsonAsync($"/api/Match/{created.Id}", badUpdateReq);
        Assert.Equal(HttpStatusCode.BadRequest, badPutResponse.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task Delete_Works_And_NotFound()
    {    // Create first
        var req = new MatchReqDto
        {
            GameId = _game1.Id,
            BeginDate = DateTime.UtcNow,

        };
        var postResponse = await _client.PostAsJsonAsync("/api/Match", req);
        var created = await postResponse.Content.ReadFromJsonAsync<MatchResDto>();
        // Delete
        var delResponse = await _client.DeleteAsync($"/api/Match/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delResponse.StatusCode);

        // Delete again (should be not found)
        var delResponse2 = await _client.DeleteAsync($"/api/Match/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, delResponse2.StatusCode);
    }
}

