using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MimicWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddStorageLinkToCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Characters_StorageId",
                table: "Characters");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_StorageId",
                table: "Characters",
                column: "StorageId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Characters_StorageId",
                table: "Characters");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_StorageId",
                table: "Characters",
                column: "StorageId");
        }
    }
}
