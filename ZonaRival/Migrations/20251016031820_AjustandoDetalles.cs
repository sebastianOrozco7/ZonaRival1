using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZonaRival.Migrations
{
    /// <inheritdoc />
    public partial class AjustandoDetalles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipoDesafiadoId",
                table: "Partidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquipoRetadorId",
                table: "Partidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_EquipoDesafiadoId",
                table: "Partidos",
                column: "EquipoDesafiadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_EquipoRetadorId",
                table: "Partidos",
                column: "EquipoRetadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidos_Equipos_EquipoDesafiadoId",
                table: "Partidos",
                column: "EquipoDesafiadoId",
                principalTable: "Equipos",
                principalColumn: "EquipoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partidos_Equipos_EquipoRetadorId",
                table: "Partidos",
                column: "EquipoRetadorId",
                principalTable: "Equipos",
                principalColumn: "EquipoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidos_Equipos_EquipoDesafiadoId",
                table: "Partidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidos_Equipos_EquipoRetadorId",
                table: "Partidos");

            migrationBuilder.DropIndex(
                name: "IX_Partidos_EquipoDesafiadoId",
                table: "Partidos");

            migrationBuilder.DropIndex(
                name: "IX_Partidos_EquipoRetadorId",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "EquipoDesafiadoId",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "EquipoRetadorId",
                table: "Partidos");
        }
    }
}
