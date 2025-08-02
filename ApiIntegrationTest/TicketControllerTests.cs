using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;

namespace ApiIntegrationTest;

public class TicketControllerTests
{
    private readonly FoxyWebApiFactory _application;
    private readonly HttpClient _client;
    private UserProfileResDto _userprofile;
    private TicketResDto _ticket;

    public TicketControllerTests()
    {
        _application = new FoxyWebApiFactory();
        _client = _application.CreateClient();
        InitializeTicketAsync().GetAwaiter().GetResult();
    }

    private async Task InitializeTicketAsync()
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
        var responseticket = await _client.GetAsync("/api/Ticket");
        var tickets = await responseticket.Content.ReadFromJsonAsync<TicketResDto[]>();
        if (tickets != null && tickets.Length >= 1)
        {
            _ticket = tickets.First();
        }
        else
        {
            var req = new TicketReqDto
            {
                Title = "Testticket",
                UserProfileId = _userprofile.Id,
                Description = "testdescription",
                CreatedAt = DateTime.Now,
                CreatedBy = _userprofile.Id,

            };
            var postResponse = await _client.PostAsJsonAsync("/api/Ticket", req);
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

            _ticket = await postResponse.Content.ReadFromJsonAsync<TicketResDto>();
        }

        return;
    }

    [Fact, TestPriority(1)]
    public async Task GetAll_ReturnsOk()
    {

        // Act
        var response = await _client.GetAsync("/api/Ticket");

        // Assert
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<TicketResDto[]>();
        //res.Length.Should().BeInRange(0, 0);
        res.Should().NotBeNull();
    }

    [Fact, TestPriority(2)]
    public async Task GetById_NotFound_Returns404()
    {
        var response = await _client.GetAsync($"/api/Ticket/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact, TestPriority(3)]
    public async Task Create_And_GetById_Works()
    {
        var req = new TicketReqDto
        {
            Title = "Testticket",
            UserProfileId = _userprofile.Id,
            Description = "testdescription",
            CreatedAt = DateTime.Now,
            CreatedBy = _userprofile.Id,
        };
        var postResponse = await _client.PostAsJsonAsync("/api/Ticket", req);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<TicketResDto>();

        Assert.NotNull(created);
        Assert.Equal(req.Title, created.Title);

        var getResponse = await _client.GetAsync($"/api/Ticket/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var getResult = await getResponse.Content.ReadFromJsonAsync<TicketResDto>();
        Assert.NotNull(getResult);
        Assert.Equal(req.Title, getResult.Title);
    }


    [Fact, TestPriority(4)]
    public async Task Update_Works_And_BadRequest_On_Id_Mismatch()
    {
        // Create first
        var req = new TicketReqDto
        {
            Title = "Toupdate",
            UserProfileId = _userprofile.Id,
            Description = "testdescription",
            CreatedAt = DateTime.Now,
            CreatedBy = _userprofile.Id,
        };
        var postResponse = await _client.PostAsJsonAsync("/api/Ticket", req);
        var created = await postResponse.Content.ReadFromJsonAsync<TicketResDto>();

        // Update with correct id
        var updateReq = new TicketReqDto
        {
            Id = created.Id,
            Title = "updated",
            UserProfileId = _userprofile.Id,
            Description = "testdescription",
            CreatedAt = created.CreatedAt,
            CreatedBy = _userprofile.Id,
            UpdateAt = DateTime.Now.ToUniversalTime(),
            UpdatedBy = _userprofile.Id,
        };
        var putResponse = await _client.PutAsJsonAsync($"/api/Ticket/{created.Id}", updateReq);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        // Update with mismatched id
        var badUpdateReq = new TicketReqDto
        {
            Id = Guid.NewGuid(),
            Title = "badrequest",
            UserProfileId = _userprofile.Id,
            Description = "testdescription",
            CreatedAt = DateTime.Now,
            CreatedBy = _userprofile.Id,
        };
        var badPutResponse = await _client.PutAsJsonAsync($"/api/Ticket/{created.Id}", badUpdateReq);
        Assert.Equal(HttpStatusCode.BadRequest, badPutResponse.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task Delete_Works_And_NotFound()
    {


        // Create first
        var req = new TicketReqDto
        {
            Title = "Todelete",
            UserProfileId = _userprofile.Id,
            Description = "testdescription",
            CreatedAt = DateTime.Now,
            CreatedBy = _userprofile.Id,
        };
        var postResponse = await _client.PostAsJsonAsync("/api/Ticket", req);
        var created = await postResponse.Content.ReadFromJsonAsync<TicketResDto>();

        // Delete
        var delResponse = await _client.DeleteAsync($"/api/Ticket/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delResponse.StatusCode);

        // Delete again (should be not found)
        var delResponse2 = await _client.DeleteAsync($"/api/Ticket/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, delResponse2.StatusCode);
    }


}


