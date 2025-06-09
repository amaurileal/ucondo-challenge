using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ucondo_challenge.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addchart_of_accounts_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChartOfAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AllowEntries = table.Column<bool>(type: "boolean", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: false),
                    ParentId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartOfAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChartOfAccounts_ChartOfAccounts_ParentId1",
                        column: x => x.ParentId1,
                        principalTable: "ChartOfAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccounts_ParentId1",
                table: "ChartOfAccounts",
                column: "ParentId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChartOfAccounts");
        }
    }
}
