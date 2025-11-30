using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G1Emergency2025.BD.Migrations
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Causas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    posibleCausa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Causas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LugarHechos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LugarHechos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Legajo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    Edad = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    HistoriaClinica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoEstados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEstados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoMovil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMovil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoTripulantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTripulantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObraSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombrePersona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacientes_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Relato = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    colorEvento = table.Column<int>(type: "int", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CausaId = table.Column<int>(type: "int", nullable: false),
                    TipoEstadoId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Causas_CausaId",
                        column: x => x.CausaId,
                        principalTable: "Causas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eventos_TipoEstados_TipoEstadoId",
                        column: x => x.TipoEstadoId,
                        principalTable: "TipoEstados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tripulantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnMovil = table.Column<bool>(type: "bit", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    TipoTripulanteId = table.Column<int>(type: "int", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tripulantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tripulantes_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tripulantes_TipoTripulantes_TipoTripulanteId",
                        column: x => x.TipoTripulanteId,
                        principalTable: "TipoTripulantes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiagPresuntivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosDiagnostico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagPresuntivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiagPresuntivos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRols",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRols", x => new { x.UsuarioId, x.RolId });
                    table.ForeignKey(
                        name: "FK_UsuarioRols_Rols_RolId",
                        column: x => x.RolId,
                        principalTable: "Rols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioRols_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventoLugarHechos",
                columns: table => new
                {
                    LugarHechoId = table.Column<int>(type: "int", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoLugarHechos", x => new { x.LugarHechoId, x.EventoId });
                    table.ForeignKey(
                        name: "FK_EventoLugarHechos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventoLugarHechos_LugarHechos_LugarHechoId",
                        column: x => x.LugarHechoId,
                        principalTable: "LugarHechos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventoUsuarios",
                columns: table => new
                {
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoUsuarios", x => new { x.EventoId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_EventoUsuarios_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventoUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistorialEventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreoEvento = table.Column<bool>(type: "bit", nullable: false),
                    ModificoEvento = table.Column<bool>(type: "bit", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialEventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialEventos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistorialEventos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    disponibilidadMovil = table.Column<int>(type: "int", nullable: false),
                    Patente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoMovilId = table.Column<int>(type: "int", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movils_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movils_TipoMovil_TipoMovilId",
                        column: x => x.TipoMovilId,
                        principalTable: "TipoMovil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PacienteEventos",
                columns: table => new
                {
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteEventos", x => new { x.PacienteId, x.EventoId });
                    table.ForeignKey(
                        name: "FK_PacienteEventos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PacienteEventos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Historicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovilId = table.Column<int>(type: "int", nullable: false),
                    TripulanteId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historicos_Movils_MovilId",
                        column: x => x.MovilId,
                        principalTable: "Movils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Historicos_Tripulantes_TripulanteId",
                        column: x => x.TripulanteId,
                        principalTable: "Tripulantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripulacionActuals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaEntrada = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    TripulanteId = table.Column<int>(type: "int", nullable: false),
                    MovilId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripulacionActuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripulacionActuals_Movils_MovilId",
                        column: x => x.MovilId,
                        principalTable: "Movils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripulacionActuals_Tripulantes_TripulanteId",
                        column: x => x.TripulanteId,
                        principalTable: "Tripulantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "Causa_UQ",
                table: "Causas",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagPresuntivos_PacienteId",
                table: "DiagPresuntivos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoLugarHechos_EventoId",
                table: "EventoLugarHechos",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "Evento_UQ",
                table: "Eventos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_CausaId",
                table: "Eventos",
                column: "CausaId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_TipoEstadoId",
                table: "Eventos",
                column: "TipoEstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoUsuarios_UsuarioId",
                table: "EventoUsuarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialEventos_EventoId",
                table: "HistorialEventos",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialEventos_UsuarioId",
                table: "HistorialEventos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Historicos_MovilId",
                table: "Historicos",
                column: "MovilId");

            migrationBuilder.CreateIndex(
                name: "IX_Historicos_TripulanteId",
                table: "Historicos",
                column: "TripulanteId");

            migrationBuilder.CreateIndex(
                name: "LugarHecho_UQ",
                table: "LugarHechos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movils_EventoId",
                table: "Movils",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Movils_TipoMovilId",
                table: "Movils",
                column: "TipoMovilId");

            migrationBuilder.CreateIndex(
                name: "Patente_UQ",
                table: "Movils",
                column: "Patente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PacienteEventos_EventoId",
                table: "PacienteEventos",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_PersonaId",
                table: "Pacientes",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "Codigo_UQ",
                table: "Rols",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Codigo_UQ",
                table: "TipoEstados",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "TipoMovil_UQ",
                table: "TipoMovil",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "TipoTripulante_UQ",
                table: "TipoTripulantes",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripulacionActuals_MovilId",
                table: "TripulacionActuals",
                column: "MovilId");

            migrationBuilder.CreateIndex(
                name: "IX_TripulacionActuals_TripulanteId",
                table: "TripulacionActuals",
                column: "TripulanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Tripulantes_PersonaId",
                table: "Tripulantes",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tripulantes_TipoTripulanteId",
                table: "Tripulantes",
                column: "TipoTripulanteId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRols_RolId",
                table: "UsuarioRols",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PersonaId",
                table: "Usuarios",
                column: "PersonaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagPresuntivos");

            migrationBuilder.DropTable(
                name: "EventoLugarHechos");

            migrationBuilder.DropTable(
                name: "EventoUsuarios");

            migrationBuilder.DropTable(
                name: "HistorialEventos");

            migrationBuilder.DropTable(
                name: "Historicos");

            migrationBuilder.DropTable(
                name: "PacienteEventos");

            migrationBuilder.DropTable(
                name: "TripulacionActuals");

            migrationBuilder.DropTable(
                name: "UsuarioRols");

            migrationBuilder.DropTable(
                name: "LugarHechos");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Movils");

            migrationBuilder.DropTable(
                name: "Tripulantes");

            migrationBuilder.DropTable(
                name: "Rols");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "TipoMovil");

            migrationBuilder.DropTable(
                name: "TipoTripulantes");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Causas");

            migrationBuilder.DropTable(
                name: "TipoEstados");
        }
    }
}
