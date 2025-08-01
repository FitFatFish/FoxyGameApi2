using FluentAssertions;
using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using Foxy.Core.Infrastructures.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiIntegrationTest;
[TestCaseOrderer("ApiIntegrationTest.TestExecutionOrderer", "ApiIntegrationTest")]

public class SuggestionControllerTests
    {
    private readonly FoxyWebApiFactory _application;
    private readonly HttpClient _client;
    private UserProfileResDto _userprofile;
    public SuggestionControllerTests()
    {
        _application = new FoxyWebApiFactory();
        _client = _application.CreateClient();
        InitializeSuggestionAsync().GetAwaiter().GetResult();
    }

    private async Task InitializeSuggestionAsync()
    {
        // Check if userpfiles already exist to avoid duplicates
        var responseuser = await _client.GetAsync("/api/UserProfile");
        var userprofiles = await responseuser.Content.ReadFromJsonAsync<UserProfileResDto[]>();
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
            _userprofile = await postResponseuser.Content.ReadFromJsonAsync<UserProfileResDto>();
        }

    }

    [Fact, TestPriority(1)]
    public async Task GetAll_ReturnsOk()
    {

        // Act
        var response = await _client.GetAsync("/api/Suggestion");

        // Assert
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<SuggestionResDto[]>();
        //res.Length.Should().BeInRange(0, 0);
        res.Should().NotBeNull();
    }

    [Fact, TestPriority(2)]
    public async Task GetById_NotFound_Returns404()
    {


        var response = await _client.GetAsync($"/api/Suggestion/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact, TestPriority(3)]
    public async Task Create_And_GetById_Works()
    {
        //AddFakeUser();


        var req = new SuggestionReqDto { 
            Title = "TestSuggestion",
            ConfirmedDescription="test" ,  
        Description="test" ,
        DislikeCount=0,
        LikeCount=0,
        Published=PublishTypeEnum.Draft,
        CreatedBy=_userprofile.Id,
        CreatedAt=DateTime.UtcNow,
        };
        var postResponse = await _client.PostAsJsonAsync("/api/Suggestion", req);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<SuggestionResDto>();
        Assert.NotNull(created);
        Assert.Equal("TestSuggestion", created.Title);

        var getResponse = await _client.GetAsync($"/api/Suggestion/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var getResult = await getResponse.Content.ReadFromJsonAsync<SuggestionResDto>();
        Assert.NotNull(getResult);
        Assert.Equal("TestSuggestion", getResult.Title);
    }


    [Fact, TestPriority(4)]
    public async Task Update_Works_And_BadRequest_On_Id_Mismatch()
    {


        // Create first
        var req = new SuggestionReqDto { 
            Title = "ToUpdate",
            ConfirmedDescription = "test",
            Description = "test",
            DislikeCount = 0,
            LikeCount = 0,
            Published = PublishTypeEnum.Draft,
            CreatedBy = _userprofile.Id,
            CreatedAt = DateTime.UtcNow,
        }; ;
        var postResponse = await _client.PostAsJsonAsync("/api/Suggestion", req);
        var created = await postResponse.Content.ReadFromJsonAsync<SuggestionResDto>();

        // Update with correct id
        var updateReq = new SuggestionReqDto { 
            Id = created.Id,
            Title = "Updated",
            ConfirmedDescription = created.ConfirmedDescription,
            Description = created.Description,
            DislikeCount =created.DislikeCount,
            LikeCount = created.LikeCount,
            Published = created.Published,
            CreatedBy = _userprofile.Id,
            CreatedAt = DateTime.UtcNow,
        };
        var putResponse = await _client.PutAsJsonAsync($"/api/Suggestion/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        // Update with mismatched id
        var badUpdateReq = new SuggestionReqDto { Id = Guid.NewGuid(), Title = "Bad" };
        var badPutResponse = await _client.PutAsJsonAsync($"/api/Suggestion/{created.Id}", badUpdateReq);
        Assert.Equal(HttpStatusCode.BadRequest, badPutResponse.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task Delete_Works_And_NotFound()
    {


        // Create first
        var req = new SuggestionReqDto
        {
            Title = "ToDelete",
            ConfirmedDescription = "test",
            Description = "test",
            DislikeCount = 0,
            LikeCount = 0,
            Published = PublishTypeEnum.Draft,
            CreatedBy = _userprofile.Id,
            CreatedAt = DateTime.UtcNow,
        }; ;
        var postResponse = await _client.PostAsJsonAsync("/api/Suggestion", req);
        var created = await postResponse.Content.ReadFromJsonAsync<SuggestionResDto>();

        // Delete
        var delResponse = await _client.DeleteAsync($"/api/Suggestion/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delResponse.StatusCode);

        // Delete again (should be not found)
        var delResponse2 = await _client.DeleteAsync($"/api/Suggestion/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, delResponse2.StatusCode);
    }

}

