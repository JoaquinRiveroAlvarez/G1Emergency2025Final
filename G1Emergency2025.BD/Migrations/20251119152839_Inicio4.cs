using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G1Emergency2025.BD.Migrations
{
    /// <inheritdoc />
    public partial class Inicio4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventoMovils",
                columns: table => new
                {
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    MovilId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoMovils", x => new { x.EventoId, x.MovilId });
                    table.ForeignKey(
                        name: "FK_EventoMovils_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventoMovils_Movils_MovilId",
                        column: x => x.MovilId,
                        principalTable: "Movils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventoMovils_MovilId",
                table: "EventoMovils",
                column: "MovilId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventoMovils");
        }
    }
}
