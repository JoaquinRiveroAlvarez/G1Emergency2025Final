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
    [Route("api/Movil")]
    public class MovilController : ControllerBase
    {
        private readonly IMovilRepositorio repositorio;
        public MovilController(IMovilRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovilListadoDTO>>> GetList()
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
        public async Task<ActionResult<MovilDTO>> GetById(int id)
        {
            var tipoProvincia = await repositorio.SelectById(id);
            if (tipoProvincia is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(tipoProvincia);
        }

        [HttpGet("Patente/{patente}")]
        public async Task<ActionResult<MovilDTO>> GetListaPatente(string patente)
        {
            var lista = await repositorio.SelectListaMovilPorPatente(patente);
            if (lista == null)
            {
                return NotFound("No se encontro la lista, VERIFICAR.");
            }
            return Ok(lista);
        }

        [HttpGet("CodTipo/{cod}")]
        public async Task<ActionResult<List<MovilDTO>>> GetListaCodTipoMovil(string cod)
        {
            var lista = await repositorio.SelectListaMovilPorCodTipoMovil(cod);
            if (lista == null)
            {
                return NotFound("No se encontro la lista, VERIFICAR.");
            }
            return Ok(lista);
        }

        [HttpGet("Tipo/{tipo}")]
        public async Task<ActionResult<List<MovilDTO>>> GetListaTipoMovil(string tipo)
        {
            var lista = await repositorio.SelectListaMovilPorTipoMovil(tipo);
            if (lista == null)
            {
                return NotFound("No se encontro la lista, VERIFICAR.");
            }
            return Ok(lista);
        }

        [HttpGet("ListaMovil")]
        public async Task<ActionResult<List<MovilListadoDTO>>> GetListaMovil()
        {
            var lista = await repositorio.SelectListaMovil();
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

        [HttpGet("ListaMovilPorDisponibilidad/{disponibilidadMovil}")]
        public async Task<ActionResult<List<MovilDTO>>> GetListaTipoMovil(DisponibilidadMovil disponibilidadMovil)
        {
            var lista = await repositorio.SelectListaMovilPorDisponibilidad(disponibilidadMovil);
            if (lista == null)
            {
                return NotFound("No se encontro la lista, VERIFICAR.");
            }
            return Ok(lista);
        }

        //[HttpGet("porDisponibilidad")]
        //public async Task<ActionResult<List<MovilesPorDisponibilidadDTO>>> GetMovilesPorDisponibilidad()
        //{
        //    var lista = await repositorio.SelectListaMovilAgrupada();
        //    if (lista == null)
        //    {
        //        return NotFound("No se encontro la lista, VERIFICAR.");
        //    }
        //    if (lista.Count == 0)
        //    {
        //        return Ok("No existen items en la lista en este momento");
        //    }
        //    return Ok(lista);
        //}

        [HttpPost]
        public async Task<ActionResult<int>> Post(MovilDTO DTO)
        {
            try
            {
                Movil entidad = new Movil
                {
                    disponibilidadMovil = DTO.disponibilidadMovil,
                    Patente = DTO.Patente
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
        public async Task<ActionResult> Put(int id, MovilDTO DTO)
        {
            var entidad = new Movil
            {
                Id = id,
                disponibilidadMovil = DTO.disponibilidadMovil,
                Patente = DTO.Patente
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
