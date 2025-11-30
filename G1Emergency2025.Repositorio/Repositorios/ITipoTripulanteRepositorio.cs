using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface ITipoTripulanteRepositorio : IRepositorio<TipoTripulante>
    {
        Task<TipoTripulante?> SelectByCod(string codigo);
        Task<List<TipoTripulanteListadoDTO>> SelectListaTipoTripulante();
    }
}