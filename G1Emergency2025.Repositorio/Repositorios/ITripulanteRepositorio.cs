using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface ITripulanteRepositorio : IRepositorio<Tripulante>
    {
        Task<Tripulante?> SelectByEnMovil(bool EnMovil);
        Task<List<TripulanteListadoDTO>> SelectListaTripulante();
    }
}