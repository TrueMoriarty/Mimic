using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MimicWebApi.Migrations
{
    /// <inheritdoc />
    public partial class RenamePropertyToItemProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "Properties",
                newName: "ItemPropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemPropertyId",
                table: "Properties",
                newName: "PropertyId");
        }
    }
}
