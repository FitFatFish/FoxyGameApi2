using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using Foxy.DataLayer.DBContext;
using Foxy.DataLayer.Models.Games;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;

namespace ApiIntegrationTest
{

    public class GameCategoryControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public GameCategoryControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        public async Task InitializeAsync()
        {
            // Wipe all data before each test
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FoxyDbContext>();
            db.GameCategories.RemoveRange(db.GameCategories);
            // Remove from other tables as needed...
            await db.SaveChangesAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private void AddFakeUser()
        {
            // Add a fake user claim for POST/PUT
            _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer faketoken");
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/GameCategory");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_And_GetById_Works()
        {
            AddFakeUser();
            var req = new GameCategoryReqDto { Title = "TestCategory" };
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
        public async Task GetById_NotFound_Returns404()
        {
            var response = await _client.GetAsync($"/api/GameCategory/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Update_Works_And_BadRequest_On_Id_Mismatch()
        {
            AddFakeUser();
            // Create first
            var req = new GameCategoryReqDto { Title = "ToUpdate" };
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
            AddFakeUser();
            // Create first
            var req = new GameCategoryReqDto { Title = "ToDelete" };
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
}
