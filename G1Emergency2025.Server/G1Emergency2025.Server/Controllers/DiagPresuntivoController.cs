using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Repositorio.Repositorios;
using G1Emergency2025.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/DiagPresuntivo")]
    public class DiagPresuntivoController : ControllerBase
    {
        private readonly IDiagPresuntivoRepositorio repositorio;

        public DiagPresuntivoController(IDiagPresuntivoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<DiagPresuntivoListadoDTO>>> GetList()
        {
            var lista = await repositorio.Select();
            if (lista == null)
            {
                return NotFound("No se encontro la lista, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen items en la lista en este momento");
            }

            return Ok(lista);
        }

        [HttpGet("Id/{id:int}")] 
        public async Task<ActionResult<DiagPresuntivoDTO>> GetById(int id)
        {
            var tipoProvincia = await repositorio.SelectById(id);
            if (tipoProvincia is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(tipoProvincia);
        }

        [HttpGet("ListaDiagPresuntivo")]
        public async Task<ActionResult<List<DiagPresuntivoListadoDTO>>> GetListaDiagPresuntivo()
        {
            var lista = await repositorio.SelectListaDiagPresuntivo();
            if (lista == null)
            {
                return NotFound("No se encontro la lista, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen items en la lista en este momento");
            }
            return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(DiagPresuntivoDTO DTO)
        {
            try
            {
                DiagPresuntivo entidad = new DiagPresuntivo
                {
                    PosDiagnostico = DTO.PosDiagnostico,
                    PacienteId = DTO.PacienteId,
                };
                var id = await repositorio.Insert(entidad);
                return Ok(entidad.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el nuevo registro: {e.Message}");
            }
        }

        [HttpPut("{id:int}")]  
        public async Task<ActionResult> Put(int id, DiagPresuntivoDTO DTO)
        {
            var entidad = new DiagPresuntivo
            {
                Id = id,
                PosDiagnostico = DTO.PosDiagnostico,
                PacienteId = DTO.PacienteId,
            };

            var resultado = await repositorio.Update(id, entidad);

            if (!resultado)
            {
                return BadRequest("Datos no válidos");
            }

            return Ok($"El registro con el id: {id} fue actualizado correctamente.");
        }

        [HttpDelete("{id:int}")] 
        public async Task<ActionResult> Delete(int id)
        {
            var resultado = await repositorio.Delete(id);
            if (!resultado)
            {
                return BadRequest("Datos no validos");
            }
            return Ok($"El registro con el id: {id} fue eliminado correctamente.");
        }
    }
}
