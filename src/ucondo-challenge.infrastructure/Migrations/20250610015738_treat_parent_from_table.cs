using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ucondo_challenge.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class treat_parent_from_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "ChartOfAccounts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId1",
                table: "ChartOfAccounts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccounts_ParentId1",
                table: "ChartOfAccounts",
                column: "ParentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartOfAccounts_ChartOfAccounts_ParentId1",
                table: "ChartOfAccounts",
                column: "ParentId1",
                principalTable: "ChartOfAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartOfAccounts_ChartOfAccounts_ParentId1",
                table: "ChartOfAccounts");

            migrationBuilder.DropIndex(
                name: "IX_ChartOfAccounts_ParentId1",
                table: "ChartOfAccounts");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ChartOfAccounts");

            migrationBuilder.DropColumn(
                name: "ParentId1",
                table: "ChartOfAccounts");
        }
    }
}
