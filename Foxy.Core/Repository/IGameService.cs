using Foxy.DataLayer.Models.FoxyGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxy.Core.Repository
{
    public interface IGameService
    {
        #region Catgory

        Task<GameCategory> GetGameCategoryById(Guid id);

        Task<List<GameCategory>> GetAllGameCategories();    

        #endregion

        #region Game
        Task<Game> GetGameById(Guid id);
        Task<List<Game>> GetAllGames(Guid? GameCatgory=null);
        #endregion

        #region Match

        Task<List<MatchGame>> GetAllMatches();
        Task<List<MatchMember>> GetAllMatchMembers(Guid? matchid=null,Guid? userid=null);

        Task AddMatch(MatchGame match);
        Task AddMachMember(MatchMember member);

        Task RemoveMatch(Guid id);

        #endregion


        #region gamescore

        Task AddUserGameScore(UserGameScore score);
        Task RemoveUserGameScore(Guid id);

        Task UpdateUserGamScore(UserGameScore score);

        Task<List<UserGameScore>> GetAllUserGameScores(Guid? userid=null,Guid? gameid=null);

        #endregion
    }
}
