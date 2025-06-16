using Foxy.Core.Repository;
using Foxy.DataLayer.Models.FoxyGame;
using Foxy.DataLayer.Models.Public;
using Foxy.WebApi.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foxy.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IStoreItemService _storeItemService;

        public HomeController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> Get() =>
         await _gameService.GetAllGames();

        [HttpGet("/GetGame/{id}")]
        public async Task<ActionResult<Game>> GetGame(Guid id)
        {
            var username = User.Identity?.Name;
            var userid = User.Claims.FirstOrDefault(c => c.Type == "userid")?.Value; ;
            var game = await _gameService.GetGameById(id);
            if (game == null)
                throw new NotFoundException($"کاربر با شناسه {id} پیدا نشد.");

            return Ok(game);
        }

        [HttpGet("/GetGameList/{catid?}")]
        public async Task<ActionResult<Game>> GetGameList(Guid? catid=null)
        {
           
            var games = await _gameService.GetAllGames();
            if (games.Any()) return Ok(games);

            return Ok(games);
        }

        [HttpGet("/GetCategoryList")]
        public async Task<ActionResult<GameCategory>> GetCategoryList()
        {

            var games = await _gameService.GetAllGameCategories();
            

            return Ok(games);
        }

        [HttpGet("/GetStoreItemList")]
        public async Task<ActionResult<GameCategory>> GetStoreItemList()
        {

            var items = await _storeItemService.GetStoreItems();
            
            return Ok(items);
        }

        [HttpPost("/StartMatch/{gameid}")]
        public async Task<ActionResult<Guid>> StartMatch(Guid gameid)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == "userid")?.Value;
            MatchGame matchGame=new MatchGame()
            {
                BeginDate = DateTime.Now.ToUniversalTime(),
                GameId=gameid,
                  };
            _gameService.AddMatch(matchGame);
            if(matchGame.Id!=null)
            {
                MatchMember matchMember = new MatchMember()
                {
                    MatchId = matchGame.Id,
                    UserId = Guid.Parse(userid),
                    ResultType = ResultTypeEnum.Undefined,
                    Score = 0,
                    TeamName = ""
                };
                _gameService.AddMachMember(matchMember);
            }
           
            if (matchGame == null)
                throw new NotFoundException($"کاربر با شناسه   پیدا نشد.");

            return Ok(matchGame.Id);
        }

    }
}
