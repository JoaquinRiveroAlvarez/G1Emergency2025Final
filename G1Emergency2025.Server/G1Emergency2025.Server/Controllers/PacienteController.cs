using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Repositorio.Repositorios;
using G1Emergency2025.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace G1Emergency2025.Server.Controllers
{
    [ApiController]
    [Route("api/paciente")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepositorio repositorio;
        public PacienteController(IPacienteRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<PacienteListadoDTO>>> GetList()
        {
            var lista = await repositorio.Select();
            if (lista == null)
            {
                return NotFound("No se encontro la lista, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok(new List<PacienteResumenDTO>());
            }

            return Ok(lista);
        }

        [HttpGet("ListaPaciente")]
        public async Task<ActionResult<List<PacienteResumenDTO>>> GetListaPaciente()
        {
            var lista = await repositorio.SelectListaPaciente();
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
        public async Task<ActionResult<PacienteDTO>> GetById(int id)
        {
            var Paciente = await repositorio.SelectById(id);
            if (Paciente is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(Paciente);
        }

        [HttpGet("detalleConPersona/{id:int}")]
        public async Task<ActionResult> ObtenerConPersona(int id)
        {
            var paciente = await repositorio.ObtenerConPersonaAsync(id);
            if (paciente == null)
                return NotFound();
            var dto = new
            {
                paciente.Id,
                paciente.ObraSocial,
                paciente.Persona?.Nombre,
                paciente.Persona?.DNI,
                paciente.Persona?.Edad,
                paciente.Persona?.Sexo,
                paciente.Persona?.Direccion
            };

            return Ok(dto);
        }
        
        [HttpPost("crearConPersona")]
        public async Task<ActionResult<int>> CrearConPersona(PacienteCrearDTO dto, [FromQuery] int eventoId)
        {
            Console.WriteLine("📌 LLEGÓ AL CONTROLLER");
            try
            {
                var persona = new Persona
                {
                    Nombre = dto.Nombre,
                    DNI = dto.DNI,
                    Direccion = dto.Direccion,
                    Sexo = dto.sexo,
                    Edad = dto.Edad,
                    Legajo = dto.Legajo,
                };

                var paciente = new Paciente
                {
                    NombrePersona = dto.Nombre,
                    ObraSocial = dto.ObraSocial,
                    HistoriaClinica = dto.HistoriaClinica
                };

                var id = await repositorio.CrearPacienteConPersona(persona, paciente, eventoId);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear paciente: {ex.Message}");
            }
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
