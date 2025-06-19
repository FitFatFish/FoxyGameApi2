using Foxy.DataLayer.Mappings;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        InstallRequiredExtension(modelBuilder);
        AssignUuidToId(modelBuilder);

        modelBuilder.ApplyConfiguration(new GameCategoryMap());
        modelBuilder.ApplyConfiguration(new GameMap());
        modelBuilder.ApplyConfiguration(new MatchMap());
        modelBuilder.ApplyConfiguration(new MatchMemberMap());
        modelBuilder.ApplyConfiguration(new StoreItemMap());
        modelBuilder.ApplyConfiguration(new UserItemMap());
        modelBuilder.ApplyConfiguration(new UserProfileMap());
        modelBuilder.ApplyConfiguration(new UserHeaderImageMap());
        modelBuilder.ApplyConfiguration(new SuggestionMap());
        modelBuilder.ApplyConfiguration(new SuggestionVoteMap());
        modelBuilder.ApplyConfiguration(new TicketMap());
    }

    private void InstallRequiredExtension(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
    }

    private void AssignUuidToId(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            var idProperty = clrType.GetProperty("Id");
            if (idProperty != null && idProperty.PropertyType == typeof(Guid))
            {
                modelBuilder.Entity(clrType)
                    .Property("Id")
                    .HasDefaultValueSql("uuid_generate_v4()");
            }
        }
    }
}

