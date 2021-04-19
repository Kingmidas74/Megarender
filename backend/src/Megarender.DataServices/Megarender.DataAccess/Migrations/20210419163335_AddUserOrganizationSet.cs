using Microsoft.EntityFrameworkCore.Migrations;

namespace Megarender.DataAccess.Migrations
{
    public partial class AddUserOrganizationSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOrganization_Organizations_OrganizationId",
                schema: "public",
                table: "UserOrganization");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOrganization_Users_UserId",
                schema: "public",
                table: "UserOrganization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOrganization",
                schema: "public",
                table: "UserOrganization");

            migrationBuilder.RenameTable(
                name: "UserOrganization",
                schema: "public",
                newName: "UserOrganizations",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_UserOrganization_UserId",
                schema: "public",
                table: "UserOrganizations",
                newName: "IX_UserOrganizations_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOrganizations",
                schema: "public",
                table: "UserOrganizations",
                columns: new[] { "OrganizationId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrganizations_Organizations_OrganizationId",
                schema: "public",
                table: "UserOrganizations",
                column: "OrganizationId",
                principalSchema: "public",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrganizations_Users_UserId",
                schema: "public",
                table: "UserOrganizations",
                column: "UserId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOrganizations_Organizations_OrganizationId",
                schema: "public",
                table: "UserOrganizations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOrganizations_Users_UserId",
                schema: "public",
                table: "UserOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOrganizations",
                schema: "public",
                table: "UserOrganizations");

            migrationBuilder.RenameTable(
                name: "UserOrganizations",
                schema: "public",
                newName: "UserOrganization",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_UserOrganizations_UserId",
                schema: "public",
                table: "UserOrganization",
                newName: "IX_UserOrganization_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOrganization",
                schema: "public",
                table: "UserOrganization",
                columns: new[] { "OrganizationId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrganization_Organizations_OrganizationId",
                schema: "public",
                table: "UserOrganization",
                column: "OrganizationId",
                principalSchema: "public",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrganization_Users_UserId",
                schema: "public",
                table: "UserOrganization",
                column: "UserId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
