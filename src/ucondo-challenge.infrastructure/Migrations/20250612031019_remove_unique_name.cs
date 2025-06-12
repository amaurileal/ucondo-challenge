using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ucondo_challenge.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class remove_unique_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_chart_of_accounts_name",
                table: "chart_of_accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_chart_of_accounts_name",
                table: "chart_of_accounts",
                column: "name",
                unique: true);
        }
    }
}
