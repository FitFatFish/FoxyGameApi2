using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxy.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initdb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameCategory_CategoryId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameCategory",
                table: "GameCategory");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "GameCategory");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "GameCategory");

            migrationBuilder.RenameTable(
                name: "GameCategory",
                newName: "GameCategories");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "GameCategories",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v4()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameCategories",
                table: "GameCategories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FoxyUserInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    AccountId = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: false),
                    ProfileImage = table.Column<string>(type: "text", nullable: false),
                    HeaderImage = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoxyUserInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeaderImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    RequiredLevel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    GameId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreItems_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    FinalDescription = table.Column<string>(type: "text", nullable: false),
                    LikeCount = table.Column<int>(type: "integer", nullable: false),
                    DislikeCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tickets_FoxyUserInfos_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "FoxyUserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGameScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    MachCount = table.Column<int>(type: "integer", nullable: false),
                    CountableMachCount = table.Column<int>(type: "integer", nullable: false),
                    WinCount = table.Column<int>(type: "integer", nullable: false),
                    LastPlayDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGameScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGameScores_FoxyUserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "FoxyUserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGameScores_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    MatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamName = table.Column<string>(type: "text", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    ResultType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchMembers_FoxyUserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "FoxyUserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchMembers_MatchGames_MatchId",
                        column: x => x.MatchId,
                        principalTable: "MatchGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreItemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserItems_FoxyUserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "FoxyUserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserItems_StoreItems_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "StoreItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuggestionVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    SuggestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Like = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionVotes_FoxyUserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "FoxyUserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuggestionVotes_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchGames_GameId",
                table: "MatchGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchMembers_MatchId",
                table: "MatchMembers",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchMembers_UserId",
                table: "MatchMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItems_GameId",
                table: "StoreItems",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionVotes_SuggestionId",
                table: "SuggestionVotes",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionVotes_UserId",
                table: "SuggestionVotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_CreatedBy",
                table: "tickets",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameScores_GameId",
                table: "UserGameScores",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameScores_UserId",
                table: "UserGameScores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_StoreItemId",
                table: "UserItems",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_UserId",
                table: "UserItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameCategories_CategoryId",
                table: "Games",
                column: "CategoryId",
                principalTable: "GameCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameCategories_CategoryId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "HeaderImages");

            migrationBuilder.DropTable(
                name: "MatchMembers");

            migrationBuilder.DropTable(
                name: "SuggestionVotes");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "UserGameScores");

            migrationBuilder.DropTable(
                name: "UserItems");

            migrationBuilder.DropTable(
                name: "MatchGames");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "FoxyUserInfos");

            migrationBuilder.DropTable(
                name: "StoreItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameCategories",
                table: "GameCategories");

            migrationBuilder.RenameTable(
                name: "GameCategories",
                newName: "GameCategory");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "GameCategory",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v4()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "GameCategory",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteBy",
                table: "GameCategory",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameCategory",
                table: "GameCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameCategory_CategoryId",
                table: "Games",
                column: "CategoryId",
                principalTable: "GameCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
