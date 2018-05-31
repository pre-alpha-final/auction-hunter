using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AuctionHunterFront.Migrations
{
	public partial class Fixingindex : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateIndex(
				name: "IX_AuctionHunterItems_AuctionLink",
				table: "AuctionHunterItems",
				column: "AuctionLink",
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				name: "IX_AuctionHunterItems_AuctionLink",
				table: "AuctionHunterItems");
		}
	}
}
