using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace G1Emergency2025.Repositorio.Repositorios
{
    public class DiagPresuntivoRepositorio : Repositorio<DiagPresuntivo>, IDiagPresuntivoRepositorio
    {
        private readonly AppDbContext context;

        public DiagPresuntivoRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<DiagPresuntivoListadoDTO>> SelectListaDiagPresuntivo()
        {
            var lista = await context.DiagPresuntivos
                                    .Select(p => new DiagPresuntivoListadoDTO
                                    {
                                        Id = p.Id,
                                        Diagnostico = $"Id Paciente: {p.PacienteId} - Posible Diagnostico: {p.PosDiagnostico}"
                                    })
                                    .ToListAsync();
            return lista;
        }
    }
}
