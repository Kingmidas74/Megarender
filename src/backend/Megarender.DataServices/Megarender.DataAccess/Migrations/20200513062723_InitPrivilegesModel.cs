using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Megarender.DataAccess.Migrations
{
    public partial class InitPrivilegesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "EntityStatus",
                schema: "public",
                columns: table => new
                {
                    EntityStatusId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityStatus", x => x.EntityStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UniqueIdentifier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Privilege",
                schema: "public",
                columns: table => new
                {
                    PrivilegeId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privilege", x => x.PrivilegeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    SurName = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessGroups",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessGroups_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "public",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserOrganization",
                schema: "public",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrganization", x => new { x.OrganizationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserOrganization_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "public",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOrganization_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessGroupPrivilege",
                schema: "public",
                columns: table => new
                {
                    AccessGroupId = table.Column<Guid>(nullable: false),
                    PrivilegeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessGroupPrivilege", x => new { x.AccessGroupId, x.PrivilegeId });
                    table.ForeignKey(
                        name: "FK_AccessGroupPrivilege_AccessGroups_AccessGroupId",
                        column: x => x.AccessGroupId,
                        principalSchema: "public",
                        principalTable: "AccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessGroupPrivilege_Privilege_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalSchema: "public",
                        principalTable: "Privilege",
                        principalColumn: "PrivilegeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessGroupUser",
                schema: "public",
                columns: table => new
                {
                    AccessGroupId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessGroupUser", x => new { x.AccessGroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AccessGroupUser_AccessGroups_AccessGroupId",
                        column: x => x.AccessGroupId,
                        principalSchema: "public",
                        principalTable: "AccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessGroupUser_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "EntityStatus",
                columns: new[] { "EntityStatusId", "Value" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Inactive" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Privilege",
                columns: new[] { "PrivilegeId", "Value" },
                values: new object[,]
                {
                    { 0, "CanAuthorize" },
                    { 1, "CanSeeScenes" },
                    { 2, "CanSeeRenderTasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessGroupPrivilege_PrivilegeId",
                schema: "public",
                table: "AccessGroupPrivilege",
                column: "PrivilegeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessGroups_OrganizationId",
                schema: "public",
                table: "AccessGroups",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessGroupUser_UserId",
                schema: "public",
                table: "AccessGroupUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganization_UserId",
                schema: "public",
                table: "UserOrganization",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessGroupPrivilege",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AccessGroupUser",
                schema: "public");

            migrationBuilder.DropTable(
                name: "EntityStatus",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UserOrganization",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Privilege",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AccessGroups",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "public");
        }
    }
}
