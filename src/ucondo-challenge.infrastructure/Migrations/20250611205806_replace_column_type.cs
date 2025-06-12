using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ucondo_challenge.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class replace_column_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "chart_of_accounts",
                type: "varchar(30)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "type",
                table: "chart_of_accounts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)");
        }
    }
}
