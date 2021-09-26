using Microsoft.EntityFrameworkCore.Migrations;

namespace Megarender.IdentityService.Migrations
{
    public partial class RemoveEmailFromUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Identities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Identities",
                type: "text",
                nullable: true);
        }
    }
}
