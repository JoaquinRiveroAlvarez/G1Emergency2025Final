using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Repositorio.Repositorios;
using G1Emergency2025.Shared.DTO;
using G1Emergency2025.Shared.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/evento")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepositorio repositorio;
        public EventoController(IEventoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<EventoDiagPresuntivoListadoDTO>> GetEventoPorId(int id)
        {
            var evento = await repositorio.SelectPorId(id);

            if (evento == null)
                return NotFound(new { mensaje = "Evento no encontrado" });

            return Ok(evento);
        }

        [HttpGet("idMovil/{id}")]
        public async Task<ActionResult<EventoDiagPresuntivoListadoDTO>> GetMovilPorId(int id)
        {
            var evento = await repositorio.SelectMovilPorId(id);

            if (evento == null)
                return NotFound(new { mensaje = "Movil no encontrado" });

            return Ok(evento);
        }

        [HttpGet("ListaMovil")]
        public async Task<IActionResult> GetListaMovil()
        {
            var lista = await repositorio.SelectListaMovil();
            if (lista == null || !lista.Any())
                return NotFound("No hay moviles registrados.");

            return Ok(lista);
        }

        [HttpGet("ListaEvento")]
        public async Task<IActionResult> GetListaEvento()
        {
            var lista = await repositorio.SelectListaEvento();
            if (lista == null || !lista.Any())
                return NotFound("No hay eventos registrados.");

            return Ok(lista);
        }

        [HttpGet("ListaEventoReciente")]
        public async Task<IActionResult> GetListaEventoReciente()
        {
            var eventos = await repositorio.SelectListaEventoReciente();
            return Ok(eventos);
        }

        [HttpGet("ListaEventoCompleto")]
        public async Task<ActionResult> GetListaEventoCompleto()
        {
            var lista = await repositorio.SelectListaEventoCompleto();
            if (lista == null || !lista.Any())
                return NotFound("No hay eventos registrados.");

            return Ok(lista);
        }
        [HttpGet("ListaEventoRecienteCompleto")]
        public async Task<ActionResult> GetListaEventoRecienteCompleto()
        {
            var lista = await repositorio.SelectListaEventoRecienteCompleto();
            if (lista == null || !lista.Any())
                return NotFound("No hay eventos registrados.");

            return Ok(lista);
        }

        [HttpGet("EventoPorCodigo/{cod}")]
        public async Task<ActionResult<List<EventoDiagPresuntivoListadoDTO>>> GetEventoCodigo(string cod)
        {
            var lista = await repositorio.SelectPorCod(cod);
            if (lista == null)
            {
                return NotFound($"No se encontro elementos en la lista con el código: {cod}.");
            }
            return Ok(lista);
        }

        [HttpGet("EventoPacientePorId/{id}")]
        public async Task<ActionResult<EventoPacienteDiagPresuntivoResumenDTO>> GetEventoPacientePorId(int id)
        {
            var lista = await repositorio.SelectEventoConPacientePorId(id);
            if (lista == null)
            {
                return NotFound($"No se encontro elementos en la lista con el código: {id}.");
            }
            return Ok(lista);
        }

        [HttpGet("ListaEventoPorHistoriaClinicaPaciente/{historiaClinica}")]
        public async Task<ActionResult<List<EventoDiagPresuntivoListadoDTO>>> GetListaEventoPorHistoriaClinicaPaciente(string historiaClinica)
        {
            var lista = await repositorio.SelectPorHistoriaClinicaPaciente(historiaClinica);
            if (lista == null)
            {
                return NotFound($"No se encontro elementos en la lista con la historia clinica: {historiaClinica}.");
            }
            return Ok(lista);
        }

        [HttpGet("EventoPorNombrePaciente")]
        public async Task<ActionResult<List<EventoDiagPresuntivoListadoDTO>>> GetEventoNombrePaciente([FromQuery] string nombre)
        {
            var lista = await repositorio.SelectPorNombrePaciente(nombre);
            if (lista == null || lista.Count == 0)
            {
                return NotFound($"No se encontro elementos en la lista con el código: {nombre}.");
            }
            return Ok(lista);
        }

        [HttpGet("EventoPorEstado/{estado}")]
        public async Task<ActionResult<List<EventoDiagPresuntivoListadoDTO>>> GetEventoEstado(int estado)
        {
            var lista = await repositorio.SelectPorTipoEstado(estado);
            if (lista == null)
            {
                return NotFound($"No se encontro elementos en la lista con el código: {estado}.");
            }
            return Ok(lista);
        }
        [HttpGet("buscarPorFecha")]
        public async Task<ActionResult<List<EventoListadoDTO>>> BuscarPorFecha(
        [FromQuery] int? anio,
        [FromQuery] int? mes,
        [FromQuery] int? dia,
        [FromQuery] int? hora)
        {
            try
            {
                var eventos = await repositorio.SelectPorFechaFlexible(anio, mes, dia, hora);

                if (!eventos.Any())
                    return NotFound("No se encontraron eventos con esos filtros.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al buscar eventos: {ex.Message}");
            }
        }

        [HttpPost("CrearEventoConPaciente")]
        public async Task<ActionResult> PostEventoPaciente([FromBody] EventoCrearDTO dto)
        {
            try
            {
                int id = await repositorio.InsertarEventoPaciente(dto);
                return Ok(id);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        [HttpPut("{id}/estadoEvento")]
        public async Task<IActionResult> ActualizarEstadoEvento(int id, [FromBody] ActualizarEstadoEventoDTO dto)
        {
            try
            {
                var actualizado = await repositorio.ActualizarEstadoEvento(id, dto.TipoEstadoId);

                if (!actualizado)
                    return NotFound(new { message = $"No se encontró el evento con ID {id}" });

                return Ok(new { estadoActualizado = dto.TipoEstadoId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}/pacienteEvento")]
        public async Task<ActionResult<bool>> ActualizarEventoPaciente(int id, EventoActualizarDTO dto)
        {
            try
            {
                return Ok(true); 
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }


        [HttpPut("{id}/AsignarMovilesEvento")]
        public async Task<ActionResult> PutAsignarMovilesEvento(int id, [FromBody] AsignarMovilesEventoDTO dto)
        {
            try
            {
                var actualizado = await repositorio.ActualizarMovilesAsignadosEvento(id, dto);

                if (!actualizado)
                    return NotFound(new { mensaje = "Evento no encontrado" });

                return Ok(new { mensaje = "Evento actualizado correctamente" });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await repositorio.DeleteEvento(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }

        //Antiguos post y put
        [HttpPost]
        public async Task<IActionResult> PostEvento([FromBody] EventoDTO dto)
        {
            try
            {
                int id = await repositorio.InsertarEvento(dto);
                return Ok(id);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, EventoDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("El Id del evento no coincide con el DTO enviado.");

            var actualizado = await repositorio.ActualizarEvento(id, dto);

            if (!actualizado)
                return NotFound($"No se encontró el evento con Id {id}");

            return Ok($"El evento con Id {id} fue actualizado correctamente.");
        }
    }
}
