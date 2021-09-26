using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Megarender.IdentityService.Migrations
{
    public partial class AddPreferredCommunicationChannel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Identities");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Identities");

            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "Identities",
                newName: "Identities",
                newSchema: "identity");

            migrationBuilder.AddColumn<int>(
                name: "PreferredCommunicationChannel",
                schema: "identity",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CommunicationChannel",
                schema: "identity",
                columns: table => new
                {
                    CommunicationChannelId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationChannel", x => x.CommunicationChannelId);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationChannelsData",
                schema: "identity",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    TelegramId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationChannelsData", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_CommunicationChannelsData_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "CommunicationChannel",
                columns: new[] { "CommunicationChannelId", "Value" },
                values: new object[,]
                {
                    { 1, "Phone" },
                    { 2, "Email" },
                    { 3, "Telegram" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunicationChannel",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "CommunicationChannelsData",
                schema: "identity");

            migrationBuilder.DropColumn(
                name: "PreferredCommunicationChannel",
                schema: "identity",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "identity",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Identities",
                schema: "identity",
                newName: "Identities");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Identities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Identities",
                type: "text",
                nullable: true);
        }
    }
}
