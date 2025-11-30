using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G1Emergency2025.BD.Migrations
{
    /// <inheritdoc />
    public partial class Inicio7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movils_Eventos_EventoId",
                table: "Movils");

            migrationBuilder.DropIndex(
                name: "IX_Movils_EventoId",
                table: "Movils");

            migrationBuilder.DropColumn(
                name: "EventoId",
                table: "Movils");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventoId",
                table: "Movils",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movils_EventoId",
                table: "Movils",
                column: "EventoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movils_Eventos_EventoId",
                table: "Movils",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id");
        }
    }
}
