using Foxy.Core.Dtos.RequestDtos;

using Foxy.DataLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Foxy.Core.Infrastructures.Enums;
using Foxy.DataLayer.Models.Games;
using Foxy.Core.Dtos.ResultDtos;

namespace ApiIntegrationTest;

[TestCaseOrderer("ApiIntegrationTest.TestExecutionOrderer", "ApiIntegrationTest")]
public class MatchMemberControllerTests
{
    private readonly FoxyWebApiFactory _application;
    private readonly HttpClient _client;
    private MatchResDto _match1;
    private MatchResDto _match2;
    private GameResDto _game1;
   
    private GameCategoryResDto _category1;
    private UserProfileResDto _userprofile;

    public MatchMemberControllerTests()
    {
        _application = new FoxyWebApiFactory();
        _client = _application.CreateClient();
        InitializeMachesAsync().GetAwaiter().GetResult();
    }

    private async Task InitializeMachesAsync()
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
        // Check if games already exist to avoid duplicates
        var response = await _client.GetAsync("/api/Match");
        var maches = await response.Content.ReadFromJsonAsync<MatchResDto[]>();
        if (maches != null && maches.Length >= 2)
        {
            _match1 = maches[0];
            _match2 = maches[1];

            return;
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
            var games = await responsegamecategory.Content.ReadFromJsonAsync<GameResDto[]>();
            if (games != null && games.Length >=1)
            {
                _game1 = games.First();


            }
            else
            {
                //create first 
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
            }

            var reqmatch1 = new Match
            {
                GameId = _game1.Id,
                BeginDate = DateTime.UtcNow,

            };
            var postResponsematch1 = await _client.PostAsJsonAsync("/api/Match", reqmatch1);
            postResponsematch1.EnsureSuccessStatusCode();
            _match1 = await postResponsematch1.Content.ReadFromJsonAsync<MatchResDto>();

            var reqmatch2 = new Match
            {
                GameId = _game1.Id,
                BeginDate = DateTime.UtcNow,

            };
            var postResponsematch2 = await _client.PostAsJsonAsync("/api/Match", reqmatch2);
            _match2 = await postResponsematch2.Content.ReadFromJsonAsync<MatchResDto>();
        }

    }


    [Fact, TestPriority(1)]
    public async Task GetAll_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/MatchMember");

        // Assert
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<MatchMemberResDto[]>();
        
        res.Should().NotBeNull();

    }

    [Fact, TestPriority(2)]
    public async Task GetById_NotFound_Returns404()
    {


        var response = await _client.GetAsync($"/api/MatchMember/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact, TestPriority(3)]
    public async Task Create_And_GetById_Works()
    {
        var req = new MatchMemberReqDto()
        {
            MatchId=_match1.Id,
            ResultType=ResultTypeEnum.Undefined,
            Score=0,
            TeamName="myteam",
            UserProfileId=_userprofile.Id,
            
        };
        var postResponse = await _client.PostAsJsonAsync("/api/MatchMember", req);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<MatchMemberResDto>();
        Assert.NotNull(created);
        Assert.Equal("myteam", created.TeamName);

        var getResponse = await _client.GetAsync($"/api/MatchMember/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var getResult = await getResponse.Content.ReadFromJsonAsync<MatchMemberResDto>();
        Assert.NotNull(getResult);
        Assert.Equal("myteam", getResult.TeamName);
    }

    [Fact, TestPriority(4)]
    public async Task Update_Works_And_BadRequest_On_Id_Mismatch()
    {
        // Create first
        var req = new MatchMemberReqDto()
        {
            MatchId = _match1.Id,
            ResultType = ResultTypeEnum.Undefined,
            Score = 0,
            TeamName = "ToUpdateTeam",
            UserProfileId = _userprofile.Id,

        };
        var postResponse = await _client.PostAsJsonAsync("/api/MatchMember", req);
        var created = await postResponse.Content.ReadFromJsonAsync<MatchMemberResDto>();

        // Update with correct id
        var updateReq = new MatchMemberReqDto()
        {
            Id=created.Id,
            MatchId = _match1.Id,
            ResultType = ResultTypeEnum.Undefined,
            Score = 0,
            TeamName = "UpdatedTeam",
            UserProfileId = _userprofile.Id,

        };
        var putResponse = await _client.PutAsJsonAsync($"/api/MatchMember/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        // Update with mismatched id
        var badUpdateReq = new MatchMemberReqDto()
        {
            Id = Guid.NewGuid(),
            MatchId = _match1.Id,
            ResultType = ResultTypeEnum.Undefined,
            Score = 0,
            TeamName = "UpdatedTeam",
            UserProfileId = _userprofile.Id,

        };
        var badPutResponse = await _client.PutAsJsonAsync($"/api/MatchMember/{created.Id}", badUpdateReq);
        Assert.Equal(HttpStatusCode.BadRequest, badPutResponse.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task Delete_Works_And_NotFound()
    {    // Create first
        var req = new MatchMemberReqDto()
        {
            
            MatchId = _match1.Id,
            ResultType = ResultTypeEnum.Undefined,
            Score = 0,
            TeamName = "MyTeam",
            UserProfileId = _userprofile.Id,

        };
        var postResponse = await _client.PostAsJsonAsync("/api/MatchMember", req);
        var created = await postResponse.Content.ReadFromJsonAsync<MatchMemberResDto>();
        // Delete
        var delResponse = await _client.DeleteAsync($"/api/MatchMember/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delResponse.StatusCode);

        // Delete again (should be not found)
        var delResponse2 = await _client.DeleteAsync($"/api/MatchMember/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, delResponse2.StatusCode);
    }

    [Fact, TestPriority(6)]
    public async Task Update_Works_ChangeMatch()
    {
        // Create first
        var req = new MatchMemberReqDto()
        {

            MatchId = _match1.Id,
            ResultType = ResultTypeEnum.Undefined,
            Score = 0,
            TeamName = "MyTeam",
            UserProfileId = _userprofile.Id,

        };
        var postResponse = await _client.PostAsJsonAsync("/api/MatchMember", req);
        var created = await postResponse.Content.ReadFromJsonAsync<MatchMemberResDto>();

        // Update with correct id
        var updateReq =new MatchMemberReqDto()
        {
            Id=created.Id,
            MatchId = _match2.Id,
            ResultType = ResultTypeEnum.Undefined,
            Score = 0,
            TeamName = "MyTeam",
            UserProfileId = _userprofile.Id,

        };
        var putResponse = await _client.PutAsJsonAsync($"/api/MatchMember/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);
    }
}

