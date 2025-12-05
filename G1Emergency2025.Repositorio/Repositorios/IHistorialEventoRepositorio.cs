using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface IHistorialEventoRepositorio : IRepositorio<HistorialEvento>
    {
        //Task<HistorialEvento?> SelectById(int id);
        Task<List<HistorialEventoListadoDTO?>> SelectListaHistorialEvento();
        Task<HistorialEventoListadoDTO?> SelectPorId(int id);
        Task RegistrarCreacionEvento(int eventoId, int usuarioId);
        Task RegistrarModificacionEvento(int eventoId, int usuarioId);
    }
}