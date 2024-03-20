using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecycLab.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_People_PersonIdPerson",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonIdPerson",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PersonIdPerson",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonIdPerson",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonIdPerson",
                table: "Users",
                column: "PersonIdPerson");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_People_PersonIdPerson",
                table: "Users",
                column: "PersonIdPerson",
                principalTable: "People",
                principalColumn: "IdPerson",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
