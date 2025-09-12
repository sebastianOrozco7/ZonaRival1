using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZonaRival.Migrations
{
    /// <inheritdoc />
    public partial class Listo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidos_Equipos_EquipoId1",
                table: "Partidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidos_Equipos_EquipoId2",
                table: "Partidos");

            migrationBuilder.DropTable(
                name: "Historiales");

            migrationBuilder.DropIndex(
                name: "IX_Partidos_EquipoId1",
                table: "Partidos");

            migrationBuilder.DropIndex(
                name: "IX_Partidos_EquipoId2",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "EquipoId1",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "EquipoId2",
                table: "Partidos");

            migrationBuilder.CreateTable(
                name: "EquiposPartidos",
                columns: table => new
                {
                    EquipoId = table.Column<int>(type: "int", nullable: false),
                    PartidoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquiposPartidos", x => new { x.EquipoId, x.PartidoId });
                    table.ForeignKey(
                        name: "FK_EquiposPartidos_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipos",
                        principalColumn: "EquipoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquiposPartidos_Partidos_PartidoId",
                        column: x => x.PartidoId,
                        principalTable: "Partidos",
                        principalColumn: "PartidoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EquiposPartidos_PartidoId",
                table: "EquiposPartidos",
                column: "PartidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquiposPartidos");

            migrationBuilder.AddColumn<int>(
                name: "EquipoId1",
                table: "Partidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquipoId2",
                table: "Partidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Historiales",
                columns: table => new
                {
                    HistorialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EquipoId = table.Column<int>(type: "int", nullable: false),
                    PartidoId = table.Column<int>(type: "int", nullable: false),
                    FechaDeRegistro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historiales", x => x.HistorialId);
                    table.ForeignKey(
                        name: "FK_Historiales_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipos",
                        principalColumn: "EquipoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historiales_Partidos_PartidoId",
                        column: x => x.PartidoId,
                        principalTable: "Partidos",
                        principalColumn: "PartidoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_EquipoId1",
                table: "Partidos",
                column: "EquipoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_EquipoId2",
                table: "Partidos",
                column: "EquipoId2");

            migrationBuilder.CreateIndex(
                name: "IX_Historiales_EquipoId",
                table: "Historiales",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Historiales_PartidoId",
                table: "Historiales",
                column: "PartidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidos_Equipos_EquipoId1",
                table: "Partidos",
                column: "EquipoId1",
                principalTable: "Equipos",
                principalColumn: "EquipoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partidos_Equipos_EquipoId2",
                table: "Partidos",
                column: "EquipoId2",
                principalTable: "Equipos",
                principalColumn: "EquipoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
