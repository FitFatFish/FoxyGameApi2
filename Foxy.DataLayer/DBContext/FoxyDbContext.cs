using Foxy.DataLayer.Models.FoxyGame;
using Foxy.DataLayer.Models.FoxyUser;
using Foxy.DataLayer.Models.Public;
using Foxy.DataLayer.Models.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Foxy.DataLayer.DBContext
{
    public class FoxyDbContext : DbContext
    {
        public FoxyDbContext(DbContextOptions<FoxyDbContext> options) : base(options)
        {


        }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameCategory>  GameCategories { get; set; }
        public DbSet<MatchGame> MatchGames { get; set; }
        public DbSet<MatchMember>  MatchMembers { get; set; }
        public DbSet<StoreItem> StoreItems { get; set; }
        public DbSet<UserGameScore> UserGameScores { get; set; }
        public DbSet<UserItem> UserItems { get; set; }
        public DbSet<FoxyUserInfo> FoxyUserInfos  { get; set; }
        public DbSet<HeaderImage> HeaderImages  { get; set; }
        public DbSet<Suggestion> Suggestions  { get; set; }
        public DbSet<SuggestionVote>  SuggestionVotes { get; set; }
        public DbSet<Ticket> tickets  { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");

            //modelBuilder.Entity<Game>()
            //    .Property(u => u.Id)
            //     .HasDefaultValueSql("uuid_generate_v4()");
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
            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    var clrType = entityType.ClrType;

            //    // پیدا کردن پراپرتی DeleteAt از نوع DateTime?
            //    var deleteAtProp = clrType.GetProperty("DeleteAt");
            //    if (deleteAtProp != null && deleteAtProp.PropertyType == typeof(DateTime?))
            //    {
            //        // ساختن عبارت lambda به صورت داینامیک: x => x.DeleteAt == null
            //        var parameter = Expression.Parameter(clrType, "x");
            //        var property = Expression.Property(parameter, "DeleteAt");
            //        var nullConstant = Expression.Constant(null, typeof(DateTime?));
            //        var equal = Expression.Equal(property, nullConstant);
            //        var lambda = Expression.Lambda(equal, parameter);

            //        modelBuilder.Entity(clrType).HasQueryFilter(lambda);
            //    }
            //}
            modelBuilder.Entity<Game>()
              .HasQueryFilter(u => u.DeleteAt == null);

            modelBuilder.Entity<StoreItem>()
          .HasOne(dt => dt.game)
         .WithOne(p => p.storeItem)
         .HasForeignKey<StoreItem>(p => p.GameId);

            modelBuilder.Entity<MatchMember>()
      .HasOne(dt => dt.match)
     .WithMany(p => p.matchMembers)
     .HasForeignKey(p => p.MatchId); 
            
            modelBuilder.Entity<MatchGame>()
          .HasOne(dt => dt.game)
         .WithMany(p => p.matches)
        .HasForeignKey(p => p.GameId);
            
            modelBuilder.Entity<MatchMember>()
          .HasOne(dt => dt.foxyUser)
         .WithMany(p => p.matchMembers)
        .HasForeignKey(p => p.UserId); 
            
            modelBuilder.Entity<UserGameScore>()
          .HasOne(dt => dt.game)
         .WithMany(p => p.gameScores)
        .HasForeignKey(p => p.GameId);   
            
            modelBuilder.Entity<UserGameScore>()
          .HasOne(dt => dt.foxyUser)
         .WithMany(p => p.gameScores)
        .HasForeignKey(p => p.UserId);   
            
            modelBuilder.Entity<UserItem>()
          .HasOne(dt => dt.foxyUser)
         .WithMany(p => p.userItems) 
        .HasForeignKey(p => p.UserId);    
            
            modelBuilder.Entity<UserItem>()
          .HasOne(dt => dt.storeItem)
         .WithMany(p => p.userItems)
        .HasForeignKey(p => p.StoreItemId); 
            
            modelBuilder.Entity<SuggestionVote>()
          .HasOne(dt => dt.suggestion)
         .WithMany(p => p.suggestionVotes)
        .HasForeignKey(p => p.SuggestionId); 
            
            modelBuilder.Entity<SuggestionVote>()
          .HasOne(dt => dt.foxyUser)
         .WithMany(p => p.suggestionVotes)
        .HasForeignKey(p => p.UserId);  
            
            modelBuilder.Entity<Ticket>()
          .HasOne(dt => dt.foxyUser)
         .WithMany(p => p.tickets)
        .HasForeignKey(p => p.CreatedBy);   
            
            modelBuilder.Entity<Game>()
          .HasOne(dt => dt.gameCategory)
         .WithMany(p => p.games)
        .HasForeignKey(p => p.CategoryId);

               
          
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            
        }
    }
}
