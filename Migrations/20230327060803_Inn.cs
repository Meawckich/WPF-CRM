using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingMaterials.Migrations
{
    /// <inheritdoc />
    public partial class Inn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Inn",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inn",
                table: "Providers");
        }
    }
}
