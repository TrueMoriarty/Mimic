using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MimicWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddStorageToCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "Characters",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_StorageId",
                table: "Characters",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Storages_StorageId",
                table: "Characters",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "StorageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Storages_StorageId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_StorageId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Characters");
        }
    }
}
