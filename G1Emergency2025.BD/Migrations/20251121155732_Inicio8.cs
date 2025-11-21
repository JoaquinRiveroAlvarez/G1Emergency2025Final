using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G1Emergency2025.BD.Migrations
{
    /// <inheritdoc />
    public partial class Inicio8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tripulantes_TipoTripulantes_TipoTripulanteId",
                table: "Tripulantes");

            migrationBuilder.AlterColumn<int>(
                name: "TipoTripulanteId",
                table: "Tripulantes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tripulantes_TipoTripulantes_TipoTripulanteId",
                table: "Tripulantes",
                column: "TipoTripulanteId",
                principalTable: "TipoTripulantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tripulantes_TipoTripulantes_TipoTripulanteId",
                table: "Tripulantes");

            migrationBuilder.AlterColumn<int>(
                name: "TipoTripulanteId",
                table: "Tripulantes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tripulantes_TipoTripulantes_TipoTripulanteId",
                table: "Tripulantes",
                column: "TipoTripulanteId",
                principalTable: "TipoTripulantes",
                principalColumn: "Id");
        }
    }
}
