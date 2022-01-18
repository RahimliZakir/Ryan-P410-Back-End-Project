using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RyanP410.WebUI.Migrations
{
    public partial class ChangesSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coding",
                table: "Coding");

            migrationBuilder.RenameTable(
                name: "Coding",
                newName: "Codings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Codings",
                table: "Codings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Codings",
                table: "Codings");

            migrationBuilder.RenameTable(
                name: "Codings",
                newName: "Coding");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coding",
                table: "Coding",
                column: "Id");
        }
    }
}
