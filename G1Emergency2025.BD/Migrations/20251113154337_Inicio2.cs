using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G1Emergency2025.BD.Migrations
{
    /// <inheritdoc />
    public partial class Inicio2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HistoriaClinica",
                table: "Persona");

            migrationBuilder.AddColumn<string>(
                name: "HistoriaClinica",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HistoriaClinica",
                table: "Pacientes");

            migrationBuilder.AddColumn<string>(
                name: "HistoriaClinica",
                table: "Persona",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
