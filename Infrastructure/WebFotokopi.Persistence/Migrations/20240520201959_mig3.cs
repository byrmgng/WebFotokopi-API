using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebFotokopi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppSellers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "AppSellers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo2",
                table: "AppSellers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppSellers");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "AppSellers");

            migrationBuilder.DropColumn(
                name: "Logo2",
                table: "AppSellers");
        }
    }
}
