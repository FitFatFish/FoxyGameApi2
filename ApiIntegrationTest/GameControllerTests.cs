using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using Foxy.DataLayer.Models.Games;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;

namespace ApiIntegrationTest
{
    public class GameControllerTests
    {



        private readonly FoxyWebApiFactory _application;
        private readonly HttpClient _client;
        private GameCategory _category1;
        private GameCategory _category2;

        public GameControllerTests()
        {
            _application = new FoxyWebApiFactory();
            _client = _application.CreateClient();
            InitializeCategoriesAsync().GetAwaiter().GetResult();
        }

        private async Task InitializeCategoriesAsync()
        {
            // Check if categories already exist to avoid duplicates
            var response = await _client.GetAsync("/api/GameCategory");
            var categories = await response.Content.ReadFromJsonAsync<GameCategory[]>();
            if (categories != null && categories.Length >= 2)
            {
                _category1 = categories[0];
                _category2 = categories[1];
                return;
            }

            // Create first category
            var req1 = new GameCategoryReqDto { Title = "Category1" };
            var postResponse1 = await _client.PostAsJsonAsync("/api/GameCategory", req1);
            postResponse1.EnsureSuccessStatusCode();
            _category1 = await postResponse1.Content.ReadFromJsonAsync<GameCategory>();

            // Create second category
            var req2 = new GameCategoryReqDto { Title = "Category2" };
            var postResponse2 = await _client.PostAsJsonAsync("/api/GameCategory", req2);
            postResponse2.EnsureSuccessStatusCode();
            _category2 = await postResponse2.Content.ReadFromJsonAsync<GameCategory>();
        }




        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            // Arrange
            var application = new FoxyWebApiFactory();
            var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Game");

            // Assert
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadFromJsonAsync<Game[]>();
            //res.Length.Should().BeInRange(0, 0);
            res.Should().NotBeNull();

        }

        [Fact]
        public async Task GetById_NotFound_Returns404()
        {
            var application = new FoxyWebApiFactory();
            var client = application.CreateClient();
            var response = await client.GetAsync($"/api/Game/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Create_And_GetById_Works()
        {
            //AddFakeUser();
            var application = new FoxyWebApiFactory();
            var client = application.CreateClient();
            //get gamecategory
            var response = await client.GetAsync("/api/GameCategory");
            var categories = await response.Content.ReadFromJsonAsync<GameCategory[]>();
            //get first
            var firstCategory = categories.First();
            var req = new GameReqDto
            {
                Title = "TestGame",
                GameCategoryId = firstCategory.Id,
                Documentation = "test",
                ImageGuid = "test"
            };
            var postResponse = await client.PostAsJsonAsync("/api/Game", req);
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

            var created = await postResponse.Content.ReadFromJsonAsync<Game>();
            Assert.NotNull(created);
            Assert.Equal("TestGame", created.Title);

            var getResponse = await client.GetAsync($"/api/Game/{created.Id}");
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var getResult = await getResponse.Content.ReadFromJsonAsync<GameCategoryResDto>();
            Assert.NotNull(getResult);
            Assert.Equal("TestGame", getResult.Title);
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
}
