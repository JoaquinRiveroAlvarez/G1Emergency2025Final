using G1Emergency2025.Repositorio.Repositorios;
using G1Emergency2025.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace G1Emergency2025.Server.Controllers
{
    [ApiController]
    [Route("api/historialEvento")]
    public class HistorialEventoController : ControllerBase
    {
        private readonly IHistorialEventoRepositorio repositorio;
        public HistorialEventoController(IHistorialEventoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<HistorialEventoListadoDTO>>> GetList()
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
        public async Task<ActionResult> SelectPorId(int id)
        {
            var entidad = await repositorio.SelectPorId(id);
            if (entidad == null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }
            return Ok(entidad);
        }

    }
}
