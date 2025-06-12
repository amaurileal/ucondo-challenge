using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ucondo_challenge.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chart_of_accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    allow_entries = table.Column<bool>(type: "boolean", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chart_of_accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_chart_of_accounts_chart_of_accounts_parent_id",
                        column: x => x.parent_id,
                        principalTable: "chart_of_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_chart_of_accounts_code",
                table: "chart_of_accounts",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_chart_of_accounts_id",
                table: "chart_of_accounts",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_chart_of_accounts_name",
                table: "chart_of_accounts",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_chart_of_accounts_parent_id",
                table: "chart_of_accounts",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_chart_of_accounts_tenant_id",
                table: "chart_of_accounts",
                column: "tenant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chart_of_accounts");
        }
    }
}
