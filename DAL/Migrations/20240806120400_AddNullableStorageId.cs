using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MimicWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableStorageId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Storages_StorageId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "StorageId",
                table: "Items",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Storages_StorageId",
                table: "Items",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "StorageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Storages_StorageId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "StorageId",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Storages_StorageId",
                table: "Items",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "StorageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
