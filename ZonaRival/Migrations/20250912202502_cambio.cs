using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZonaRival.Migrations
{
    /// <inheritdoc />
    public partial class cambio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disponibilidad",
                table: "Equipos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disponibilidad",
                table: "Equipos");
        }
    }
}
