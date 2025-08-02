using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using Foxy.Core.Infrastructures.Enums;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;

namespace ApiIntegrationTest;
[TestCaseOrderer("ApiIntegrationTest.TestExecutionOrderer", "ApiIntegrationTest")]
public class SuggestionVoteControllerTests
{
    private readonly FoxyWebApiFactory _application;
    private readonly HttpClient _client;
    private UserProfileResDto _userprofile;
    private SuggestionResDto _suggestion;
    private SuggestionVoteResDto _suggestionvote;
    public SuggestionVoteControllerTests()
    {
        _application = new FoxyWebApiFactory();
        _client = _application.CreateClient();
        InitializeSuggestionVoteAsync().GetAwaiter().GetResult();
    }

    private async Task InitializeSuggestionVoteAsync()
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
        var responsesuggestion = await _client.GetAsync("/api/Suggestion");
        var suggestions = await responsesuggestion.Content.ReadFromJsonAsync<SuggestionResDto[]>();
        if (suggestions != null && suggestions.Length >= 1)
        {
            _suggestion = suggestions[0];
        }
        else
        {
            var req = new SuggestionReqDto
            {
                Title = "TestSuggestion",
                ConfirmedDescription = "test",
                Description = "test",
                DislikeCount = 0,
                LikeCount = 0,
                Published = PublishTypeEnum.Draft,
                CreatedBy = _userprofile.Id,
                CreatedAt = DateTime.UtcNow,
            };
            var postResponse = await _client.PostAsJsonAsync("/api/Suggestion", req);
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

            _suggestion = await postResponse.Content.ReadFromJsonAsync<SuggestionResDto>();
        }

        return;
    }

    [Fact, TestPriority(1)]
    public async Task GetAll_ReturnsOk()
    {

        // Act
        var response = await _client.GetAsync("/api/SuggestionVote");

        // Assert
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<SuggestionVoteResDto[]>();
        //res.Length.Should().BeInRange(0, 0);
        res.Should().NotBeNull();
    }

    [Fact, TestPriority(2)]
    public async Task GetById_NotFound_Returns404()
    {
        var response = await _client.GetAsync($"/api/SuggestionVote/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact, TestPriority(3)]
    public async Task Create_And_GetById_Works()
    {
        var req = new SuggestionVoteReqDto
        {
            SuggestionId = _suggestion.Id,
            Like = true,
            UserProfileId = _userprofile.Id,

        };
        var postResponse = await _client.PostAsJsonAsync("/api/SuggestionVote", req);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<SuggestionVoteResDto>();

        Assert.NotNull(created);
        Assert.Equal(req.SuggestionId, created.SuggestionId);

        var getResponse = await _client.GetAsync($"/api/SuggestionVote/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var getResult = await getResponse.Content.ReadFromJsonAsync<SuggestionVoteResDto>();
        Assert.NotNull(getResult);
        Assert.Equal(req.SuggestionId, getResult.SuggestionId);
    }


    [Fact, TestPriority(4)]
    public async Task Update_Works_And_BadRequest_On_Id_Mismatch()
    {
        // Create first
        var req = new SuggestionVoteReqDto
        {
            SuggestionId = _suggestion.Id,
            Like = true,
            UserProfileId = _userprofile.Id,
        };
        var postResponse = await _client.PostAsJsonAsync("/api/SuggestionVote", req);
        var created = await postResponse.Content.ReadFromJsonAsync<SuggestionVoteResDto>();

        // Update with correct id
        var updateReq = new SuggestionVoteReqDto
        {
            Id = created.Id,
            SuggestionId = _suggestion.Id,
            Like = false,
            UserProfileId = _userprofile.Id,
        };
        var putResponse = await _client.PutAsJsonAsync($"/api/SuggestionVote/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        // Update with mismatched id
        var badUpdateReq = new SuggestionVoteReqDto
        {
            Id = Guid.NewGuid(),
            SuggestionId = _suggestion.Id,
            Like = false,
            UserProfileId = _userprofile.Id,
        };
        var badPutResponse = await _client.PutAsJsonAsync($"/api/SuggestionVote/{created.Id}", badUpdateReq);
        Assert.Equal(HttpStatusCode.BadRequest, badPutResponse.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task Delete_Works_And_NotFound()
    {


        // Create first
        var req = new SuggestionVoteReqDto
        {
            SuggestionId = _suggestion.Id,
            Like = false,
            UserProfileId = _userprofile.Id,
        };
        var postResponse = await _client.PostAsJsonAsync("/api/SuggestionVote", req);
        var created = await postResponse.Content.ReadFromJsonAsync<SuggestionVoteResDto>();

        // Delete
        var delResponse = await _client.DeleteAsync($"/api/SuggestionVote/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delResponse.StatusCode);

        // Delete again (should be not found)
        var delResponse2 = await _client.DeleteAsync($"/api/SuggestionVote/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, delResponse2.StatusCode);
    }


}

