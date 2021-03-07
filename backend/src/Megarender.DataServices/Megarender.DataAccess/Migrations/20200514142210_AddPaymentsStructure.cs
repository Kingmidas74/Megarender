using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Megarender.DataAccess.Migrations
{
    public partial class AddPaymentsStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoneyTransactionStatus",
                schema: "public",
                columns: table => new
                {
                    MoneyTransactionStatusId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTransactionStatus", x => x.MoneyTransactionStatusId);
                });

            migrationBuilder.CreateTable(
                name: "PrivateMoneyTransactions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    MoneyTransactionStatus = table.Column<int>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateMoneyTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateMoneyTransactions_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharedMoneyTransactions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    MoneyTransactionStatus = table.Column<int>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedMoneyTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedMoneyTransactions_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharedMoneyTransactions_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "public",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "MoneyTransactionStatus",
                columns: new[] { "MoneyTransactionStatusId", "Value" },
                values: new object[,]
                {
                    { 1, "Created" },
                    { 2, "Approved" },
                    { 3, "Deposited" },
                    { 4, "Declined" },
                    { 5, "Reversed" },
                    { 6, "Refunded" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrivateMoneyTransactions_CreatedById",
                schema: "public",
                table: "PrivateMoneyTransactions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SharedMoneyTransactions_CreatedById",
                schema: "public",
                table: "SharedMoneyTransactions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SharedMoneyTransactions_OrganizationId",
                schema: "public",
                table: "SharedMoneyTransactions",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyTransactionStatus",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PrivateMoneyTransactions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "SharedMoneyTransactions",
                schema: "public");
        }
    }
}
