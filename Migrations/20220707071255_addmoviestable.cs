using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_movia.Migrations
{
    public partial class addmoviestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    rate = table.Column<double>(type: "float", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    Storyline = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    poster = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    generaid = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.id);
                    table.ForeignKey(
                        name: "FK_movies_generas_generaid",
                        column: x => x.generaid,
                        principalTable: "generas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_movies_generaid",
                table: "movies",
                column: "generaid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movies");
        }
    }
}
