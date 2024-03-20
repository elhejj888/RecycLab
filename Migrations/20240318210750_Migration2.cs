using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecycLab.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collectors_People_PersonIdPerson",
                table: "Collectors");

            migrationBuilder.DropIndex(
                name: "IX_Collectors_PersonIdPerson",
                table: "Collectors");

            migrationBuilder.RenameColumn(
                name: "PersonIdPerson",
                table: "Collectors",
                newName: "PersonTempId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonTempId1",
                table: "Collectors",
                newName: "PersonIdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_Collectors_PersonIdPerson",
                table: "Collectors",
                column: "PersonIdPerson");

            migrationBuilder.AddForeignKey(
                name: "FK_Collectors_People_PersonIdPerson",
                table: "Collectors",
                column: "PersonIdPerson",
                principalTable: "People",
                principalColumn: "IdPerson",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
