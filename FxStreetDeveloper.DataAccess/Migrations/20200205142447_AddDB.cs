using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FxStreetDeveloper.DataAccess.Migrations
{
    public partial class AddDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TeamName = table.Column<string>(nullable: true),
                    YellowCards = table.Column<int>(nullable: false),
                    RedCards = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Manager_PK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    TeamName = table.Column<string>(nullable: true),
                    YellowCards = table.Column<int>(nullable: false),
                    RedCards = table.Column<int>(nullable: false),
                    MinutesPlayed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Player_PK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Referees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MinutesPlayed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Referee_PK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matchs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    HouseTeamManagerId = table.Column<Guid>(nullable: true),
                    AwayTeamManagerId = table.Column<Guid>(nullable: true),
                    RefereeId = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Match_PK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matchs_Managers_AwayTeamManagerId",
                        column: x => x.AwayTeamManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matchs_Managers_HouseTeamManagerId",
                        column: x => x.HouseTeamManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matchs_Referees_RefereeId",
                        column: x => x.RefereeId,
                        principalTable: "Referees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchsPlayersAway",
                columns: table => new
                {
                    MatchId = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchsPlayersAway", x => new { x.MatchId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_MatchsPlayersAway_Matchs_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchsPlayersAway_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchsPlayersHouse",
                columns: table => new
                {
                    MatchId = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchsPlayersHouse", x => new { x.MatchId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_MatchsPlayersHouse_Matchs_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchsPlayersHouse_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_AwayTeamManagerId",
                table: "Matchs",
                column: "AwayTeamManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_HouseTeamManagerId",
                table: "Matchs",
                column: "HouseTeamManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_RefereeId",
                table: "Matchs",
                column: "RefereeId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchsPlayersAway_PlayerId",
                table: "MatchsPlayersAway",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchsPlayersHouse_PlayerId",
                table: "MatchsPlayersHouse",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchsPlayersAway");

            migrationBuilder.DropTable(
                name: "MatchsPlayersHouse");

            migrationBuilder.DropTable(
                name: "Matchs");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Referees");
        }
    }
}
