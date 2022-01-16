using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RyanP410.WebUI.Migrations
{
    public partial class Pricings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PricingDetails_Pricings_PricingId",
                table: "PricingDetails");

            migrationBuilder.DropIndex(
                name: "IX_PricingDetails_PricingId",
                table: "PricingDetails");

            migrationBuilder.DropColumn(
                name: "Exists",
                table: "PricingDetails");

            migrationBuilder.DropColumn(
                name: "New",
                table: "PricingDetails");

            migrationBuilder.DropColumn(
                name: "PricingId",
                table: "PricingDetails");

            migrationBuilder.CreateTable(
                name: "PricingsPricingDetailsCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricingId = table.Column<int>(type: "int", nullable: false),
                    PricingDetailId = table.Column<int>(type: "int", nullable: false),
                    Exists = table.Column<bool>(type: "bit", nullable: false),
                    New = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingsPricingDetailsCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricingsPricingDetailsCollections_PricingDetails_PricingDetailId",
                        column: x => x.PricingDetailId,
                        principalTable: "PricingDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PricingsPricingDetailsCollections_Pricings_PricingId",
                        column: x => x.PricingId,
                        principalTable: "Pricings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PricingsPricingDetailsCollections_PricingDetailId",
                table: "PricingsPricingDetailsCollections",
                column: "PricingDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PricingsPricingDetailsCollections_PricingId",
                table: "PricingsPricingDetailsCollections",
                column: "PricingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PricingsPricingDetailsCollections");

            migrationBuilder.AddColumn<bool>(
                name: "Exists",
                table: "PricingDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "New",
                table: "PricingDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PricingId",
                table: "PricingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PricingDetails_PricingId",
                table: "PricingDetails",
                column: "PricingId");

            migrationBuilder.AddForeignKey(
                name: "FK_PricingDetails_Pricings_PricingId",
                table: "PricingDetails",
                column: "PricingId",
                principalTable: "Pricings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
