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

            // Armo la respuesta combinando Paciente + Persona
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
        //[HttpPost]
        //public async Task<ActionResult<int>> Post(PacienteDTO DTO)
        //{
        //    try
        //    {
        //        Paciente entidad = new Paciente
        //        {
        //            ObraSocial = DTO.ObraSocial,
        //            PersonaId = DTO.PersonaId
        //        };
        //        var id = await repositorio.Insert(entidad);
        //        return Ok(entidad.Id);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest($"Error al crear el nuevo registro: {e.Message}");
        //    }
        //}

        //[HttpPost("crearConPersona")]
        //public async Task<ActionResult<int>> CrearConPersona(PacienteCrearDTO dto)
        //{
        //    Console.WriteLine("📌 LLEGÓ AL CONTROLLER");
        //    try
        //    {
        //        var persona = new Persona
        //        {
        //            Nombre = dto.Nombre,
        //            DNI = dto.DNI,
        //            Direccion = dto.Direccion,
        //            Sexo = dto.sexo,
        //            Edad = dto.Edad,
        //            Legajo = dto.Legajo,
        //        };

        //        var paciente = new Paciente
        //        {
        //            NombrePersona = dto.Nombre,
        //            ObraSocial = dto.ObraSocial,
        //            HistoriaClinica = dto.HistoriaClinica
        //        };

        //        var id = await repositorio.CrearPacienteConPersona(persona, paciente);
        //        return Ok(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al crear paciente: {ex.Message}");
        //    }
        //}
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
        //[HttpPut("{id:int}")]
        //public async Task<ActionResult> Put(int id, PacienteDTO DTO)
        //{
        //    var entidad = new Paciente
        //    {
        //        Id = id,
        //        ObraSocial = DTO.ObraSocial!,
        //        NombrePersona = DTO.NombrePersona!,
        //        PersonaId = DTO.PersonaId
        //    };

        //    var resultado = await repositorio.Update(id, entidad);

        //    if (!resultado)
        //    {
        //        return BadRequest("Datos no válidos");
        //    }

        //    return Ok($"El registro con el id: {id} fue actualizado correctamente.");
        //}
        //[HttpPut("{id:int}/ActualizarConEventos")]
        //public async Task<IActionResult> Put(int id, PacienteActualizarDTO dto)
        //{
        //    var resultado = await repositorio.UpdatePacienteConEventos(id, dto);

        //    if (!resultado)
        //        return BadRequest("No se pudo actualizar el paciente. Verifique los datos enviados.");

        //    return Ok($"Paciente con ID {id} y sus eventos asociados fueron actualizados correctamente.");
        //}


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
