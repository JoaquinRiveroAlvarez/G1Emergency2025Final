using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface ITripulacionActualRepositorio : IRepositorio<TripulacionActual>
    {
        Task<TripulacionActual?> SelectByFechaEntrada(DateTime FechaEntrada);
        Task<List<TripulacionActualListadoDTO>> SelectListaTripulacionActual();
    }
}