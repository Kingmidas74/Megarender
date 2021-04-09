using Microsoft.EntityFrameworkCore.Migrations;

namespace Megarender.DataAccess.Migrations
{
    public partial class AddUniqueConstraintToOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Organizations_UniqueIdentifier",
                schema: "public",
                table: "Organizations",
                column: "UniqueIdentifier",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Organizations_UniqueIdentifier",
                schema: "public",
                table: "Organizations");
        }
    }
}
