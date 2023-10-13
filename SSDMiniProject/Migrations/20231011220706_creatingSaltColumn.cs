using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSDMiniProject.Migrations
{
    /// <inheritdoc />
    public partial class creatingSaltColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MasterPassword",
                table: "UserAccounts",
                newName: "HashedPassword");

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "UserAccounts",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "UserAccounts");

            migrationBuilder.RenameColumn(
                name: "HashedPassword",
                table: "UserAccounts",
                newName: "MasterPassword");
        }
    }
}
