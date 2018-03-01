using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AuctionHunterFront.Migrations
{
    public partial class MovingMarkasreadreasponsibilitytodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkedAsRead",
                table: "AuctionHunterItems");

            migrationBuilder.CreateTable(
                name: "ApplicationUserAuctionHunterItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    AuctionHunterItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserAuctionHunterItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserAuctionHunterItems_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserAuctionHunterItems_AuctionHunterItems_AuctionHunterItemId",
                        column: x => x.AuctionHunterItemId,
                        principalTable: "AuctionHunterItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserAuctionHunterItems_ApplicationUserId",
                table: "ApplicationUserAuctionHunterItems",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserAuctionHunterItems_AuctionHunterItemId",
                table: "ApplicationUserAuctionHunterItems",
                column: "AuctionHunterItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserAuctionHunterItems");

            migrationBuilder.AddColumn<bool>(
                name: "MarkedAsRead",
                table: "AuctionHunterItems",
                nullable: false,
                defaultValue: false);
        }
    }
}
