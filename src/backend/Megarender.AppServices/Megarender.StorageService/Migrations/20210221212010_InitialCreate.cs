using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Megarender.StorageService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StorageSystem",
                columns: table => new
                {
                    StorageSystemId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageSystem", x => x.StorageSystemId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StorageSystem = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "StorageSystem",
                columns: new[] { "StorageSystemId", "Value" },
                values: new object[,]
                {
                    { 1, "FTP" },
                    { 2, "AZURE" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorageSystem");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
