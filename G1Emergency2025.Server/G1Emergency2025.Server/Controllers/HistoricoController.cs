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
    [Route("api/Historico")]
    public class HistoricoController : ControllerBase
    {
        private readonly IHistoricoRepositorio repositorio;
        public HistoricoController(IHistoricoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Historico>>> GetList()
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
        public async Task<ActionResult<HistoricoDTO>> GetById(int id)
        {
            var tipoProvincia = await repositorio.SelectById(id);
            if (tipoProvincia is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(tipoProvincia);
        }
        [HttpGet("FechaEntrada/{FechaEntrada:datetime}")]
        public async Task<ActionResult<HistoricoDTO>> GetByFechaEntrada(DateTime FechaEntrada)
        {
            var tipoProvincia = await repositorio.SelectByFechaEntrada(FechaEntrada);
            if (tipoProvincia is null)
            {
                return NotFound($"No existe el registro con la fecha de entrada: {FechaEntrada}.");
            }

            return Ok(tipoProvincia);
        }
        [HttpGet("FechaSalida/{FechaSalida:datetime}")]
        public async Task<ActionResult<HistoricoDTO>> GetByFechaSalida(DateTime FechaSalida)
        {
            var tipoProvincia = await repositorio.SelectByFechaSalida(FechaSalida);
            if (tipoProvincia is null)
            {
                return NotFound($"No existe el registro con la fecha de salida: {FechaSalida}.");
            }

            return Ok(tipoProvincia);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(HistoricoDTO DTO)
        {
            try
            {
                Historico entidad = new Historico
                {
                    FechaEntrada = DateTime.Parse(DTO.FechaEntrada),
                    FechaSalida = DateTime.Parse(DTO.FechaSalida),
                    MovilId = DTO.MovilId,
                    TripulanteId = DTO.TripulanteId
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
        public async Task<ActionResult> Put(int id, HistoricoDTO DTO)
        {
            var entidad = new Historico
            {
                Id = id,
                FechaEntrada = DateTime.Parse(DTO.FechaEntrada),
                FechaSalida = DateTime.Parse(DTO.FechaSalida),
                MovilId = DTO.MovilId,
                TripulanteId = DTO.TripulanteId
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
