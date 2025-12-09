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
        [HttpGet("id/{id}")]
        public async Task<ActionResult<PacienteResumenDTO>> GetPacientePorId(int id)
        {
            var paciente = await repositorio.SelectPorId(id);

            if (paciente == null)
                return NotFound(new { mensaje = "Paciente no encontrado" });

            return Ok(paciente);
        }
        [HttpGet("DNIPersona/{dni}")]
        public async Task<ActionResult<PacienteDTO>> GetPacientePorDNIPersona(int dni)
        {
            var paciente = await repositorio.SelectPorDNIPersona(dni);

            if (paciente == null)
                return NotFound(new { mensaje = "Paciente no encontrado" });

            return Ok(paciente);
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

        [HttpGet("ListaPacientePersonaEvento")]
        public async Task<ActionResult<List<PacienteResumenDTO>>> SelectListaPacienteCompleto()
        {
            var lista = await repositorio.SelectListaPacienteConPersonaYEvento();
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

        [HttpGet("NombrePacienteEvento")]
        public async Task<ActionResult> GetNombrePacienteConEventos(string nombre)
        {
            var paciente = await repositorio.SelectNombrePacienteConPersonaYEvento(nombre);

            if (paciente == null)
                return NotFound(new { message = $"No se encontró el paciente con el nombre: {nombre}" });

            return Ok(paciente);
        }

        [HttpGet("HistoriaClinicaPacienteEvento")]
        public async Task<ActionResult> GetHistoriaClinicaPacienteConEventos(string historiaClinica)
        {
            var paciente = await repositorio.SelectHistoriaClinicaPacienteConPersonaYEvento(historiaClinica);

            if (paciente == null)
                return NotFound(new { message = $"No se encontró el paciente con la historia clinica: {historiaClinica}" });

            return Ok(paciente);
        }

        [HttpGet("DNIPacienteEvento")]
        public async Task<ActionResult> GetDNIPacienteConEventos(string cod)
        {
            var paciente = await repositorio.SelectDNIPacienteConPersonaYEvento(cod);

            if (paciente == null)
                return NotFound(new { message = $"No se encontró el paciente con con el DNI: {cod}" });

            return Ok(paciente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePaciente(int id, [FromBody] PacienteActualizarDTO dto)
        {
            var result = await repositorio.UpdatePacienteConEventos(id, dto);

            if (!result)
                return NotFound(new { message = $"No se encontró el paciente con id {id}" });

            return Ok(new { message = $"Paciente {id} actualizado correctamente" });
        }

        [HttpPost("crearConPersona")]
        public async Task<ActionResult> CrearPaciente([FromBody] PacienteCrearDTO dto)
        {
            try
            {
                var id = await repositorio.CrearPacienteConPersona(dto);
                return Ok(new { message = $"Paciente creado correctamente con Id {id}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al crear el paciente", detail = ex.Message });
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
