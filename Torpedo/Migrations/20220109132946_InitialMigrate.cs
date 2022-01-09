using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Torpedo.Migrations
{
    public partial class InitialMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScoreBoard",
                columns: table => new
                {
                    Date = table.Column<DateTime>(nullable: false),
                    Turns = table.Column<int>(nullable: false),
                    Winner = table.Column<string>(nullable: true),
                    Player1 = table.Column<string>(maxLength: 20, nullable: true),
                    Player1Score = table.Column<int>(nullable: false),
                    Player2 = table.Column<string>(maxLength: 20, nullable: true),
                    Player2Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreBoard", x => x.Date);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoreBoard");
        }
    }
}
