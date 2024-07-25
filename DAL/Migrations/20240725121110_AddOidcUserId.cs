using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MimicWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddOidcUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OidcUserId",
                table: "Users",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OidcUserId",
                table: "Users");
        }
    }
}
