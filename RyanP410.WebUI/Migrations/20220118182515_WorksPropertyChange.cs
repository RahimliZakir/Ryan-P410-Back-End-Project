using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RyanP410.WebUI.Migrations
{
    public partial class WorksPropertyChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Works_CategoryId",
                table: "Works",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Categories_CategoryId",
                table: "Works",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Categories_CategoryId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_CategoryId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Works");
        }
    }
}
