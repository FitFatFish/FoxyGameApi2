using Foxy.Core.Repository;
using Foxy.DataLayer.DBContext;
using Foxy.DataLayer.Models.FoxyGame;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Foxy.Core.Services
{
    public class GameService : IGameService
    {
        private FoxyDbContext _dbContext;

        public GameService(FoxyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddMachMember(MatchMember member)
        {
            _dbContext.Add(member);
            _dbContext.SaveChanges();
        }

        public async Task AddMatch(MatchGame match)
        {
            try
            {
                _dbContext.Add(match);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public async Task AddUserGameScore(UserGameScore score)
        {
            _dbContext.Add(score);
            _dbContext.SaveChanges();
        }

        public async Task<List<GameCategory>> GetAllGameCategories()
        {
            IQueryable<GameCategory> items = _dbContext.GameCategories;

            

            return await items.ToListAsync();
        }

        public async Task<List<Game>> GetAllGames(Guid? GameCatgory = null)
        {
            IQueryable<Game> items = _dbContext.Games;

            if(GameCatgory != null)
            {
                items= items.Where(x=>x.CategoryId== GameCatgory);
            }

            return await items.ToListAsync();
        }

        public async Task<List<MatchGame>> GetAllMatches()
        {
            IQueryable<MatchGame> items = _dbContext.MatchGames;



            return await items.ToListAsync();
        }

        public async  Task<List<MatchMember>> GetAllMatchMembers(Guid? matchid = null, Guid? userid = null)
        {
            IQueryable<MatchMember> items = _dbContext.MatchMembers;

            if(matchid != null)
            {
                items=items.Where(x=>x.MatchId==matchid);
            }

            if (userid != null)
            {
                items = items.Where(x => x.UserId == userid);
            }

            return await items.ToListAsync();
        }

        public async Task<List<UserGameScore>> GetAllUserGameScores(Guid? userid = null, Guid? gameid = null)
        {
            IQueryable<UserGameScore> items = _dbContext.UserGameScores;

            if (gameid != null)
            {
                items = items.Where(x => x.GameId == gameid);
            }

            if (userid != null)
            {
                items = items.Where(x => x.UserId == userid);
            }

            return await items.ToListAsync();
        }

        public async Task<Game> GetGameById(Guid id)
        {
            return await _dbContext.Games.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<GameCategory> GetGameCategoryById(Guid id)
        {
            return await _dbContext.GameCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task RemoveMatch(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserGameScore(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserGamScore(UserGameScore score)
        {
            _dbContext.Update(score);
            _dbContext.SaveChanges();
        }
    }
}
