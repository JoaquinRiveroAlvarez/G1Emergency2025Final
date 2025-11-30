using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Repositorio.Repositorios;
using G1Emergency2025.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/tipoEstado")]
    public class TipoEstadoController : ControllerBase
    {
        private readonly ITipoEstadoRepositorio repositorio;
        public TipoEstadoController(ITipoEstadoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoEstadoListadoDTO>>> GetList()
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
        public async Task<ActionResult<TipoEstadoDTO>> GetById(int id)
        {
            var Rol = await repositorio.SelectById(id);
            if (Rol is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(Rol);
        }

        [HttpGet("Codigo/{cod}")]
        public async Task<ActionResult<TipoEstadoDTO>> GetByCod(string cod)
        {
            var rol = await repositorio.SelectByCod(cod);
            if (rol is null)
            {
                return NotFound($"No existe el registro con el código: {cod}.");
            }

            return Ok(rol);
        }

        [HttpGet("ListaTipoEstado")]
        public async Task<ActionResult<List<TipoEstadoListadoDTO>>> GetListaTipoEstado()
        {
            var lista = await repositorio.SelectListaTipoEstado();
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
        public async Task<ActionResult<int>> Post(TipoEstadoDTO DTO)
        {
            try
            {
                TipoEstado entidad = new TipoEstado
                {
                    Codigo = DTO.Codigo,
                    Tipo = DTO.Tipo
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
        public async Task<ActionResult> Put(int id, TipoEstadoDTO DTO)
        {
            var entidad = new TipoEstado
            {
                Id = id,
                Codigo = DTO.Codigo,
                Tipo = DTO.Tipo
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
