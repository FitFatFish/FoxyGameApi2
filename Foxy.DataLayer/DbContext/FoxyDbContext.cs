//using Foxy.DataLayer.Models.FoxyGame;
//using Foxy.DataLayer.Models.FoxyUser;
//using Foxy.DataLayer.Models.Games;
//using Foxy.DataLayer.Models.Generals;
//using Foxy.DataLayer.Models.Public;
//using Foxy.DataLayer.Models.Support;
//using Foxy.DataLayer.Models.Users;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection.Emit;
//using System.Text;
//using System.Threading.Tasks;

using Foxy.DataLayer.Models.Games;
using Foxy.DataLayer.Models.Generals;
using Foxy.DataLayer.Models.Support;
using Foxy.DataLayer.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Foxy.DataLayer.DBContext;
public class FoxyDbContext : DbContext
{
    public FoxyDbContext(DbContextOptions<FoxyDbContext> options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<GameCategory> GameCategories { get; set; }
    public DbSet<Match> MatchGames { get; set; }
    public DbSet<MatchMember> MatchMembers { get; set; }
    public DbSet<StoreItem> StoreItems { get; set; }
    //public DbSet<UserGameScore> UserGameScores { get; set; }
    public DbSet<UserItem> UserItems { get; set; }
    public DbSet<UserProfile> FoxyUserInfos { get; set; }
    public DbSet<UserHeaderImage> HeaderImages { get; set; }
    public DbSet<Suggestion> Suggestions { get; set; }
    public DbSet<SuggestionVote> SuggestionVotes { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
}

