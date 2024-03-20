using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecycLab.Migrations
{
    /// <inheritdoc />
    public partial class Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Confirmations");

            migrationBuilder.AddColumn<bool>(
                name: "isConfirmed",
                table: "Transactions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isConfirmed",
                table: "Transactions");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Confirmations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
