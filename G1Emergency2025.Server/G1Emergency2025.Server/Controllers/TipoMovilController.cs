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
    [Route("api/TipoMovil")]
    public class TipoMovilController : ControllerBase
    {
        private readonly ITipoMovilRepositorio repositorio;
        public TipoMovilController(ITipoMovilRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoMovilListadoDTO>>> GetList()
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
        public async Task<ActionResult<TipoMovilDTO>> GetById(int id)
        {
            var tipoProvincia = await repositorio.SelectById(id);
            if (tipoProvincia is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(tipoProvincia);
        }

        [HttpGet("Codigo/{cod}")]
        public async Task<ActionResult<CausaListadoDTO>> GetByCod(string cod)
        {
            var tipoProvincia = await repositorio.SelectByCod(cod);
            if (tipoProvincia is null)
            {
                return NotFound($"No existe el registro con el código: {cod}.");
            }

            return Ok(tipoProvincia);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(TipoMovilDTO DTO)
        {

            try
            {
                TipoMovil entidad = new TipoMovil
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
        public async Task<ActionResult> Put(int id, TipoMovilDTO DTO)
        {
            var entidad = new TipoMovil
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
