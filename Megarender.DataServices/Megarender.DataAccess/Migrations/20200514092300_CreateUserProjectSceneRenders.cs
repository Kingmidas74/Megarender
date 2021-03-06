using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class CreateUserProjectSceneRenders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                schema: "public",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                schema: "public",
                table: "AccessGroups",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "public",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationProject",
                schema: "public",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationProject", x => new { x.ProjectId, x.OrganizationId });
                    table.ForeignKey(
                        name: "FK_OrganizationProject_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "public",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationProject_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "public",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scenes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scenes_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Scenes_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "public",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProject",
                schema: "public",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProject", x => new { x.ProjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserProject_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "public",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProject_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Renders",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    SceneId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Renders_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Renders_Scenes_SceneId",
                        column: x => x.SceneId,
                        principalSchema: "public",
                        principalTable: "Scenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_CreatedById",
                schema: "public",
                table: "Organizations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccessGroups_CreatedById",
                schema: "public",
                table: "AccessGroups",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProject_OrganizationId",
                schema: "public",
                table: "OrganizationProject",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedById",
                schema: "public",
                table: "Projects",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OrganizationId",
                schema: "public",
                table: "Projects",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Renders_CreatedById",
                schema: "public",
                table: "Renders",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Renders_SceneId",
                schema: "public",
                table: "Renders",
                column: "SceneId");

            migrationBuilder.CreateIndex(
                name: "IX_Scenes_CreatedById",
                schema: "public",
                table: "Scenes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Scenes_ProjectId",
                schema: "public",
                table: "Scenes",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_UserId",
                schema: "public",
                table: "UserProject",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessGroups_Users_CreatedById",
                schema: "public",
                table: "AccessGroups",
                column: "CreatedById",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Users_CreatedById",
                schema: "public",
                table: "Organizations",
                column: "CreatedById",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessGroups_Users_CreatedById",
                schema: "public",
                table: "AccessGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Users_CreatedById",
                schema: "public",
                table: "Organizations");

            migrationBuilder.DropTable(
                name: "OrganizationProject",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Renders",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UserProject",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Scenes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_CreatedById",
                schema: "public",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_AccessGroups_CreatedById",
                schema: "public",
                table: "AccessGroups");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                schema: "public",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                schema: "public",
                table: "AccessGroups");
        }
    }
}
