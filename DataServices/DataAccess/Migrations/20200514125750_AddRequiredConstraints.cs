using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddRequiredConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessGroups_Users_CreatedById",
                schema: "public",
                table: "AccessGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessGroups_Organizations_OrganizationId",
                schema: "public",
                table: "AccessGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Users_CreatedById",
                schema: "public",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_CreatedById",
                schema: "public",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Renders_Users_CreatedById",
                schema: "public",
                table: "Renders");

            migrationBuilder.DropForeignKey(
                name: "FK_Renders_Scenes_SceneId",
                schema: "public",
                table: "Renders");

            migrationBuilder.DropForeignKey(
                name: "FK_Scenes_Users_CreatedById",
                schema: "public",
                table: "Scenes");

            migrationBuilder.DropForeignKey(
                name: "FK_Scenes_Projects_ProjectId",
                schema: "public",
                table: "Scenes");

            migrationBuilder.AlterColumn<string>(
                name: "SurName",
                schema: "public",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "public",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "public",
                table: "Scenes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "public",
                table: "Scenes",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                schema: "public",
                table: "Scenes",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "public",
                table: "Renders",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SceneId",
                schema: "public",
                table: "Renders",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                schema: "public",
                table: "Renders",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "public",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                schema: "public",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UniqueIdentifier",
                schema: "public",
                table: "Organizations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                schema: "public",
                table: "Organizations",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                schema: "public",
                table: "AccessGroups",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                schema: "public",
                table: "AccessGroups",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessGroups_Users_CreatedById",
                schema: "public",
                table: "AccessGroups",
                column: "CreatedById",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessGroups_Organizations_OrganizationId",
                schema: "public",
                table: "AccessGroups",
                column: "OrganizationId",
                principalSchema: "public",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Users_CreatedById",
                schema: "public",
                table: "Organizations",
                column: "CreatedById",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_CreatedById",
                schema: "public",
                table: "Projects",
                column: "CreatedById",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Renders_Users_CreatedById",
                schema: "public",
                table: "Renders",
                column: "CreatedById",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Renders_Scenes_SceneId",
                schema: "public",
                table: "Renders",
                column: "SceneId",
                principalSchema: "public",
                principalTable: "Scenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scenes_Users_CreatedById",
                schema: "public",
                table: "Scenes",
                column: "CreatedById",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scenes_Projects_ProjectId",
                schema: "public",
                table: "Scenes",
                column: "ProjectId",
                principalSchema: "public",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessGroups_Users_CreatedById",
                schema: "public",
                table: "AccessGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessGroups_Organizations_OrganizationId",
                schema: "public",
                table: "AccessGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Users_CreatedById",
                schema: "public",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_CreatedById",
                schema: "public",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Renders_Users_CreatedById",
                schema: "public",
                table: "Renders");

            migrationBuilder.DropForeignKey(
                name: "FK_Renders_Scenes_SceneId",
                schema: "public",
                table: "Renders");

            migrationBuilder.DropForeignKey(
                name: "FK_Scenes_Users_CreatedById",
                schema: "public",
                table: "Scenes");

            migrationBuilder.DropForeignKey(
                name: "FK_Scenes_Projects_ProjectId",
                schema: "public",
                table: "Scenes");

            migrationBuilder.AlterColumn<string>(
                name: "SurName",
                schema: "public",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "public",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "public",
                table: "Scenes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "public",
                table: "Scenes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                schema: "public",
                table: "Scenes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "public",
                table: "Renders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "SceneId",
                schema: "public",
                table: "Renders",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                schema: "public",
                table: "Renders",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "public",
                table: "Projects",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                schema: "public",
                table: "Projects",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "UniqueIdentifier",
                schema: "public",
                table: "Organizations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                schema: "public",
                table: "Organizations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                schema: "public",
                table: "AccessGroups",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                schema: "public",
                table: "AccessGroups",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

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
                name: "FK_AccessGroups_Organizations_OrganizationId",
                schema: "public",
                table: "AccessGroups",
                column: "OrganizationId",
                principalSchema: "public",
                principalTable: "Organizations",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_CreatedById",
                schema: "public",
                table: "Projects",
                column: "CreatedById",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Renders_Users_CreatedById",
                schema: "public",
                table: "Renders",
                column: "CreatedById",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Renders_Scenes_SceneId",
                schema: "public",
                table: "Renders",
                column: "SceneId",
                principalSchema: "public",
                principalTable: "Scenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Scenes_Users_CreatedById",
                schema: "public",
                table: "Scenes",
                column: "CreatedById",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Scenes_Projects_ProjectId",
                schema: "public",
                table: "Scenes",
                column: "ProjectId",
                principalSchema: "public",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
