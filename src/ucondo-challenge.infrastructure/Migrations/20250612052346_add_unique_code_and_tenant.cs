using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ucondo_challenge.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_unique_code_and_tenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_chart_of_accounts_code",
                table: "chart_of_accounts");

            migrationBuilder.CreateIndex(
                name: "IX_chart_of_accounts_tenant_id_code",
                table: "chart_of_accounts",
                columns: new[] { "tenant_id", "code" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_chart_of_accounts_tenant_id_code",
                table: "chart_of_accounts");

            migrationBuilder.CreateIndex(
                name: "IX_chart_of_accounts_code",
                table: "chart_of_accounts",
                column: "code",
                unique: true);
        }
    }
}
