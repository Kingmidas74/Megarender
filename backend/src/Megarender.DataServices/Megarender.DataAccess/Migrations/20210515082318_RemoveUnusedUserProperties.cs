using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Megarender.DataAccess.Migrations
{
    public partial class RemoveUnusedUserProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthdate",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecondName",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SurName",
                schema: "public",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                schema: "public",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "public",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                schema: "public",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurName",
                schema: "public",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
