using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G1Emergency2025.BD.Migrations
{
    /// <inheritdoc />
    public partial class FinalNM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoRegistro",
                table: "PacienteEventos",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EstadoRegistro",
                table: "EventoUsuarios",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EstadoRegistro",
                table: "EventoMovils",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EstadoRegistro",
                table: "EventoLugarHechos",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoRegistro",
                table: "PacienteEventos");

            migrationBuilder.DropColumn(
                name: "EstadoRegistro",
                table: "EventoUsuarios");

            migrationBuilder.DropColumn(
                name: "EstadoRegistro",
                table: "EventoMovils");

            migrationBuilder.DropColumn(
                name: "EstadoRegistro",
                table: "EventoLugarHechos");
        }
    }
}
