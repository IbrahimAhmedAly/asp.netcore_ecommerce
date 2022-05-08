using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace book.Migrations
{
    public partial class addCreationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "TbItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "TbItems");
        }
    }
}
