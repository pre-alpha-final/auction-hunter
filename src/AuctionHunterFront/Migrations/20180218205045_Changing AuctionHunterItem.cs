using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AuctionHunterFront.Migrations
{
    public partial class ChangingAuctionHunterItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditionalDataJson",
                table: "AuctionHunterItems",
                newName: "ContentJson");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentJson",
                table: "AuctionHunterItems",
                newName: "AdditionalDataJson");
        }
    }
}
