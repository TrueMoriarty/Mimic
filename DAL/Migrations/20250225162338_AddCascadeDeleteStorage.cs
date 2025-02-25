using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MimicWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Storages_StorageId",
                table: "Characters");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Storages_StorageId",
                table: "Characters",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "StorageId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Storages_StorageId",
                table: "Characters");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Storages_StorageId",
                table: "Characters",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "StorageId");
        }
    }
}
