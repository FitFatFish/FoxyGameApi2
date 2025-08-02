using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;
using Foxy.Core.Infrastructures.Enums;

namespace ApiIntegrationTest;

public class UserItemControllerTests
{
    private readonly FoxyWebApiFactory _application;
    private readonly HttpClient _client;
    private UserProfileResDto _userprofile;
    private UserItemResDto _useritem;
    private StoreItemResDto _storeitem;
    private StoreItemResDto _storeitem2;
    private GameResDto _game1;

    private GameCategoryResDto _category1;
    public UserItemControllerTests()
    {
        _application = new FoxyWebApiFactory();
        _client = _application.CreateClient();
        InitializeUserItemAsync().GetAwaiter().GetResult();
    }

    private async Task InitializeUserItemAsync()
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
        var responsestoreitem = await _client.GetAsync("/api/StoreItem");
        var storeitems = await responsestoreitem.Content.ReadFromJsonAsync<StoreItemResDto[]>();
        if (storeitems != null && storeitems.Length >= 2)
        {
            _storeitem = storeitems[0];
            _storeitem2 = storeitems[1];
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
            //check game already exist
            var responsegame = await _client.GetAsync("/api/Game");
            var games = await responsegame.Content.ReadFromJsonAsync<GameResDto[]>();
            if (games != null && games.Length >= 1)
            {
                _game1 = games.First();


            }
            else
            {
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
            }

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

            var reqstoreitem2 = new StoreItemReqDto
            {
                GameId = _game1.Id,
                ImageGuid = Guid.NewGuid().ToString(),
                Level = 1,
                Title = "TestStoreItem2",
                Type = StoreItemTypeEnum.None,
                CreatedAt = DateTime.Now,
                UpdatedBy = _userprofile.Id,
            };
            var postResponsestoreitem2 = await _client.PostAsJsonAsync("/api/StoreItem", reqstoreitem2);
            _storeitem2 = await postResponsestoreitem2.Content.ReadFromJsonAsync<StoreItemResDto>();
        }
        var responseuseritem = await _client.GetAsync("/api/UserItem");
        var useritems = await responseuseritem.Content.ReadFromJsonAsync<UserItemResDto[]>();
        if (useritems != null && useritems.Length >= 1)
        {
            _useritem = useritems.First();
        }
        else
        {
          
            var req = new UserItemReqDto
            {
                UserProfileId = _userprofile.Id,
                StoreItemId = _storeitem.Id,

            };
            var postResponse = await _client.PostAsJsonAsync("/api/UserItem", req);
            _useritem = await postResponse.Content.ReadFromJsonAsync<UserItemResDto>();
        }

        return;
    }

    [Fact, TestPriority(1)]
    public async Task GetAll_ReturnsOk()
    {

        // Act
        var response = await _client.GetAsync("/api/UserItem");

        // Assert
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<UserItemResDto[]>();
        //res.Length.Should().BeInRange(0, 0);
        res.Should().NotBeNull();
    }

    [Fact, TestPriority(2)]
    public async Task GetById_NotFound_Returns404()
    {
        var response = await _client.GetAsync($"/api/UserItem/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact, TestPriority(3)]
    public async Task Create_And_GetById_Works()
    {
        var req = new UserItemReqDto
        {
            UserProfileId = _userprofile.Id,
            StoreItemId = _storeitem.Id,
        };
        var postResponse = await _client.PostAsJsonAsync("/api/UserItem", req);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<UserItemResDto>();

        Assert.NotNull(created);
        Assert.Equal(req.StoreItemId, created.StoreItemId);

        var getResponse = await _client.GetAsync($"/api/UserItem/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var getResult = await getResponse.Content.ReadFromJsonAsync<UserItemResDto>();
        Assert.NotNull(getResult);
        Assert.Equal(req.StoreItemId, getResult.StoreItemId);
    }


    [Fact, TestPriority(4)]
    public async Task Update_Works_And_BadRequest_On_Id_Mismatch()
    {
        // Create first
        var req = new UserItemReqDto
        {
            UserProfileId = _userprofile.Id,
            StoreItemId = _storeitem.Id,
        };
        var postResponse = await _client.PostAsJsonAsync("/api/UserItem", req);
        var created = await postResponse.Content.ReadFromJsonAsync<UserItemResDto>();

        // Update with correct id
        var updateReq = new UserItemReqDto
        {
            Id = created.Id,
            UserProfileId = _userprofile.Id,
            StoreItemId = _storeitem.Id,
        };
        var putResponse = await _client.PutAsJsonAsync($"/api/UserItem/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        // Update with mismatched id
        var badUpdateReq = new UserItemReqDto
        {
            Id = Guid.NewGuid(),
            UserProfileId = _userprofile.Id,
            StoreItemId = _storeitem.Id,
        };
        var badPutResponse = await _client.PutAsJsonAsync($"/api/UserItem/{created.Id}", badUpdateReq);
        Assert.Equal(HttpStatusCode.BadRequest, badPutResponse.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task Delete_Works_And_NotFound()
    {


        // Create first
        var req = new UserItemReqDto
        {
            UserProfileId = _userprofile.Id,
            StoreItemId = _storeitem.Id,
        };
        var postResponse = await _client.PostAsJsonAsync("/api/UserItem", req);
        var created = await postResponse.Content.ReadFromJsonAsync<UserItemResDto>();

        // Delete
        var delResponse = await _client.DeleteAsync($"/api/UserItem/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delResponse.StatusCode);

        // Delete again (should be not found)
        var delResponse2 = await _client.DeleteAsync($"/api/UserItem/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, delResponse2.StatusCode);
    }

    [Fact, TestPriority(6)]
    public async Task Update_Works_When_StoreItemUpdated()
    {
        // Create first
        var req = new UserItemReqDto
        {
            UserProfileId = _userprofile.Id,
            StoreItemId = _storeitem.Id,
        };
        var postResponse = await _client.PostAsJsonAsync("/api/UserItem", req);
        var created = await postResponse.Content.ReadFromJsonAsync<UserItemResDto>();

        // Update with correct id
        var updateReq = new UserItemReqDto
        {
            Id = created.Id,
            UserProfileId = _userprofile.Id,
            StoreItemId = _storeitem2.Id,
        };
        var putResponse = await _client.PutAsJsonAsync($"/api/UserItem/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);
    }
}

