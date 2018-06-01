using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AuctionHunterFront.Migrations.AuctionHunterDb
{
    public partial class AuctionHunterDbContextSeparatingidentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionHunterItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AuctionLink = table.Column<string>(maxLength: 250, nullable: false),
                    ContentJson = table.Column<string>(nullable: true),
                    OnPage = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionHunterItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserAuctionHunterItems",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    AuctionHunterItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserAuctionHunterItems", x => new { x.ApplicationUserId, x.AuctionHunterItemId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserAuctionHunterItems_AuctionHunterItems_AuctionHunterItemId",
                        column: x => x.AuctionHunterItemId,
                        principalTable: "AuctionHunterItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserAuctionHunterItems_AuctionHunterItemId",
                table: "ApplicationUserAuctionHunterItems",
                column: "AuctionHunterItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionHunterItems_AuctionLink",
                table: "AuctionHunterItems",
                column: "AuctionLink",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserAuctionHunterItems");

            migrationBuilder.DropTable(
                name: "AuctionHunterItems");
        }
    }
}
