using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MimicWebApi.Migrations
{
    /// <inheritdoc />
    public partial class RenamePropertyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PropertiesId",
                table: "Properties",
                newName: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "Properties",
                newName: "PropertiesId");
        }
    }
}
