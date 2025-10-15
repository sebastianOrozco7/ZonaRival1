using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZonaRival.Migrations
{
    /// <inheritdoc />
    public partial class AtributoHoraEnPartido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hora",
                table: "Partidos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hora",
                table: "Partidos");
        }
    }
}
