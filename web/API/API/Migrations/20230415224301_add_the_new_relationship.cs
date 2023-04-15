using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class add_the_new_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Level_Semesters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LevelId = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    SemesterId = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level_Semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Level_Semesters_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Level_Semesters_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Level_Semesters_LevelId",
                table: "Level_Semesters",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Level_Semesters_SemesterId",
                table: "Level_Semesters",
                column: "SemesterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Level_Semesters");
        }
    }
}
