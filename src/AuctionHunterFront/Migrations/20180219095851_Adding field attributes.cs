using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AuctionHunterFront.Migrations
{
	public partial class Addingfieldattributes : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "ID",
				table: "AuctionHunterItems",
				newName: "Id");

			migrationBuilder.AlterColumn<string>(
				name: "AuctionLink",
				table: "AuctionHunterItems",
				maxLength: 250,
				nullable: false,
				oldClrType: typeof(string),
				oldNullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "Id",
				table: "AuctionHunterItems",
				newName: "ID");

			migrationBuilder.AlterColumn<string>(
				name: "AuctionLink",
				table: "AuctionHunterItems",
				nullable: true,
				oldClrType: typeof(string),
				oldMaxLength: 250);
		}
	}
}
