using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using System.Net.Http.Json;
using System.Net;
using Foxy.Core.Infrastructures.Enums;
using FluentAssertions;

namespace ApiIntegrationTest;
public class StoreItemControllerTests
{
    private readonly FoxyWebApiFactory _application;
    private readonly HttpClient _client;
    private UserProfileResDto _userprofile;
    private GameResDto _game1;
    private GameResDto _game2;
    private StoreItemResDto _storeitem;
    private GameCategoryResDto _category1;

    public StoreItemControllerTests()
    {
        _application = new FoxyWebApiFactory();
        _client = _application.CreateClient();
        InitializeStoreItemAsync().GetAwaiter().GetResult();
    }

    private async Task InitializeStoreItemAsync()
    {
        // Check if userpfiles already exist to avoid duplicates
        var responseuser = await _client.GetAsync("/api/UserProfile");
        var userprofiles = await responseuser.Content.ReadFromJsonAsync<UserProfileResDto[]>();
        if (userprofiles != null && userprofiles.Length >= 1)
        {
            _userprofile = userprofiles[0];


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
        var responsegame = await _client.GetAsync("/api/Game");
        var games = await responsegame.Content.ReadFromJsonAsync<GameResDto[]>();
        if (games != null && games.Length >= 2)
        {
            _game1 = games[0];
            _game2 = games[1];


        }
        else
        {
            //check gamecategory already exist
            var responsegamecategory = await _client.GetAsync("/api/GameCategory");
            var categories = await responsegamecategory.Content.ReadFromJsonAsync<GameCategoryResDto[]>();
            if (categories != null && categories.Length >= 1)
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

            //create first 
            var reqgame = new GameReqDto
            {
                Title = "TestGame",
                GameCategoryId = _category1.Id,
                CreatedBy = _userprofile.Id,
                Documentation = "test",
                ImageGuid = "test",

            };
            var postResponsegame = await _client.PostAsJsonAsync("/api/Game", reqgame);
            postResponsegame.EnsureSuccessStatusCode();
            _game1 = await postResponsegame.Content.ReadFromJsonAsync<GameResDto>();
            var reqgame2 = new GameReqDto
            {
                Title = "TestGame",
                GameCategoryId = _category1.Id,
                CreatedBy = _userprofile.Id,
                Documentation = "test",
                ImageGuid = "test",

            };
            var postResponsegame2 = await _client.PostAsJsonAsync("/api/Game", reqgame2);
            postResponsegame2.EnsureSuccessStatusCode();
            _game2 = await postResponsegame2.Content.ReadFromJsonAsync<GameResDto>();

            var reqstoreitem = new StoreItemReqDto
            {
                GameId = _game1.Id,
                ImageGuid = Guid.NewGuid().ToString(),
                Level = 1,
                Title = "TestStoreItem",
                Type = StoreItemTypeEnum.None,
                CreatedAt = DateTime.Now,
                UpdatedBy = _userprofile.Id,
            };
            var postResponsestoreitem = await _client.PostAsJsonAsync("/api/StoreItem", reqstoreitem);
            _storeitem = await postResponsestoreitem.Content.ReadFromJsonAsync<StoreItemResDto>();


        }

        return;
    }

    [Fact, TestPriority(1)]
    public async Task GetAll_ReturnsOk()
    {

        // Act
        var response = await _client.GetAsync("/api/StoreItem");

        // Assert
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<StoreItemResDto[]>();
        //res.Length.Should().BeInRange(0, 0);
        res.Should().NotBeNull();
    }

    [Fact, TestPriority(2)]
    public async Task GetById_NotFound_Returns404()
    {
        var response = await _client.GetAsync($"/api/StoreItem/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact, TestPriority(3)]
    public async Task Create_And_GetById_Works()
    {
        var req = new StoreItemReqDto
        {
            GameId = _game1.Id,
            ImageGuid = Guid.NewGuid().ToString(),
            Level = 1,
            Title = "TestStoreItem",
            Type = StoreItemTypeEnum.None,
            CreatedAt = DateTime.Now,
            UpdatedBy = _userprofile.Id,
        };
        var postResponse = await _client.PostAsJsonAsync("/api/StoreItem", req);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<StoreItemResDto>();

        Assert.NotNull(created);
        Assert.Equal(req.Title, created.Title);

        var getResponse = await _client.GetAsync($"/api/StoreItem/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var getResult = await getResponse.Content.ReadFromJsonAsync<StoreItemResDto>();
        Assert.NotNull(getResult);
        Assert.Equal(req.Title, getResult.Title);
    }


    [Fact, TestPriority(4)]
    public async Task Update_Works_And_BadRequest_On_Id_Mismatch()
    {
        // Create first
        var req = new StoreItemReqDto
        {
            Title = "Toupdate",
            GameId = _game1.Id,
            ImageGuid = Guid.NewGuid().ToString(),
            Level = 1,
            Type = StoreItemTypeEnum.None,
            CreatedAt = DateTime.Now,
            CreatedBy = _userprofile.Id,

        };
        var postResponse = await _client.PostAsJsonAsync("/api/StoreItem", req);
        var created = await postResponse.Content.ReadFromJsonAsync<StoreItemResDto>();

        // Update with correct id
        var updateReq = new StoreItemReqDto
        {
            Id = created.Id,
            Title = "updated",
            GameId = _game1.Id,
            ImageGuid = Guid.NewGuid().ToString(),
            Level = 1,
            Type = StoreItemTypeEnum.None,
            CreatedAt = created.CreatedAt,
            CreatedBy = created.CreatedBy,
            UpdatedBy = _userprofile.Id,
            UpdateAt = DateTime.Now.ToUniversalTime(),
        };
        var putResponse = await _client.PutAsJsonAsync($"/api/StoreItem/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        // Update with mismatched id
        var badUpdateReq = new StoreItemReqDto
        {
            Id = Guid.NewGuid(),
            Title = "badrequest",
            GameId = _game1.Id,
            ImageGuid = Guid.NewGuid().ToString(),
            Level = 1,
            Type = StoreItemTypeEnum.None,
            CreatedAt = DateTime.Now,
            UpdatedBy = _userprofile.Id,
        };
        var badPutResponse = await _client.PutAsJsonAsync($"/api/StoreItem/{created.Id}", badUpdateReq);
        Assert.Equal(HttpStatusCode.BadRequest, badPutResponse.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task Delete_Works_And_NotFound()
    {


        // Create first
        var req = new StoreItemReqDto
        {
            Title = "Todelete",
            GameId = _game1.Id,
            ImageGuid = Guid.NewGuid().ToString(),
            Level = 1,
            Type = StoreItemTypeEnum.None,
            CreatedAt = DateTime.Now,
            UpdatedBy = _userprofile.Id,
        };
        var postResponse = await _client.PostAsJsonAsync("/api/StoreItem", req);
        var created = await postResponse.Content.ReadFromJsonAsync<StoreItemResDto>();

        // Delete
        var delResponse = await _client.DeleteAsync($"/api/StoreItem/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delResponse.StatusCode);

        // Delete again (should be not found)
        var delResponse2 = await _client.DeleteAsync($"/api/StoreItem/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, delResponse2.StatusCode);
    }

    [Fact, TestPriority(6)]
    public async Task Update_Works_When_GameIdUpdated()
    {   // Create first
        var req = new StoreItemReqDto
        {
            Title = "Toupdate",
            GameId = _game1.Id,
            ImageGuid = Guid.NewGuid().ToString(),
            Level = 1,
            Type = StoreItemTypeEnum.None,
            CreatedAt = DateTime.Now,
            CreatedBy = _userprofile.Id,

        };
        var postResponse = await _client.PostAsJsonAsync("/api/StoreItem", req);
        var created = await postResponse.Content.ReadFromJsonAsync<StoreItemResDto>();

        // Update with correct id
        var updateReq = new StoreItemReqDto
        {
            Id = created.Id,
            Title = created.Title,
            GameId = _game2.Id,
            ImageGuid = created.ImageGuid,
            Level = created.Level,
            Type = created.Type,
            CreatedAt = created.CreatedAt,
            CreatedBy = created.CreatedBy,
            UpdatedBy = _userprofile.Id,
            UpdateAt = DateTime.Now.ToUniversalTime(),
        };
        var putResponse = await _client.PutAsJsonAsync($"/api/StoreItem/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

    }

    [Fact, TestPriority(7)]
    public async Task Update_Works_When_LevelUpdated()
    {   // Create first
        var req = new StoreItemReqDto
        {
            Title = "Toupdate",
            GameId = _game1.Id,
            ImageGuid = Guid.NewGuid().ToString(),
            Level = 1,
            Type = StoreItemTypeEnum.None,
            CreatedAt = DateTime.Now,
            CreatedBy = _userprofile.Id,

        };
        var postResponse = await _client.PostAsJsonAsync("/api/StoreItem", req);
        var created = await postResponse.Content.ReadFromJsonAsync<StoreItemResDto>();

        // Update with correct id
        var updateReq = new StoreItemReqDto
        {
            Id = created.Id,
            Title = created.Title,
            GameId = created.GameId,
            ImageGuid = created.ImageGuid,
            Level = 2,
            Type = created.Type,
            CreatedAt = created.CreatedAt,
            CreatedBy = created.CreatedBy,
            UpdatedBy = _userprofile.Id,
            UpdateAt = DateTime.Now.ToUniversalTime(),
        };
        var putResponse = await _client.PutAsJsonAsync($"/api/StoreItem/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

    }

    [Fact, TestPriority(8)]
    public async Task Update_Works_When_TypeUpdated()
    {   // Create first
        var req = new StoreItemReqDto
        {
            Title = "Toupdate",
            GameId = _game1.Id,
            ImageGuid = Guid.NewGuid().ToString(),
            Level = 1,
            Type = StoreItemTypeEnum.None,
            CreatedAt = DateTime.Now,
            CreatedBy = _userprofile.Id,

        };
        var postResponse = await _client.PostAsJsonAsync("/api/StoreItem", req);
        var created = await postResponse.Content.ReadFromJsonAsync<StoreItemResDto>();

        // Update with correct id
        var updateReq = new StoreItemReqDto
        {
            Id = created.Id,
            Title = created.Title,
            GameId = created.GameId,
            ImageGuid = created.ImageGuid,
            Level = created.Level,
            Type = StoreItemTypeEnum.Special,
            CreatedAt = created.CreatedAt,
            CreatedBy = created.CreatedBy,
            UpdatedBy = _userprofile.Id,
            UpdateAt = DateTime.Now.ToUniversalTime(),
        };
        var putResponse = await _client.PutAsJsonAsync($"/api/StoreItem/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

    }
}

