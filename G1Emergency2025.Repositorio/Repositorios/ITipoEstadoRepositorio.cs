using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface ITipoEstadoRepositorio : IRepositorio<TipoEstado>
    {
        Task<TipoEstado?> SelectByCod(string cod);
        Task<List<TipoEstadoListadoDTO>> SelectListaTipoEstado();
    }
}