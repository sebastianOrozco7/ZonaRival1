using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZonaRival.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEquipoEdad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Equipos_EquipoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_EquipoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EquipoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "RangoEdadMaxima",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "RangoEdadMinima",
                table: "Equipos");

            migrationBuilder.AddColumn<string>(
                name: "RangoEdad",
                table: "Equipos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdEquipo",
                table: "Usuarios",
                column: "IdEquipo");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Equipos_IdEquipo",
                table: "Usuarios",
                column: "IdEquipo",
                principalTable: "Equipos",
                principalColumn: "EquipoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Equipos_IdEquipo",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_IdEquipo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "RangoEdad",
                table: "Equipos");

            migrationBuilder.AddColumn<int>(
                name: "EquipoId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RangoEdadMaxima",
                table: "Equipos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RangoEdadMinima",
                table: "Equipos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EquipoId",
                table: "Usuarios",
                column: "EquipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Equipos_EquipoId",
                table: "Usuarios",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "EquipoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
