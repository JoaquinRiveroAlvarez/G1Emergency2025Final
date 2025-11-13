using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface IEventoRepositorio : IRepositorio<Evento>
    {
        Task<Evento?> SelectByCod(string cod);
        Task<List<EventoListadoDTO>> SelectListaEventoReciente();
        Task<List<EventoListadoDTO>> SelectListaEvento();
        Task<EventoListadoDTO?> SelectListaPorCod(string cod);
        Task<int> InsertarEvento(EventoDTO dto);
        Task<bool> ActualizarEvento(int id, EventoDTO dto);
        Task<bool> DeleteEvento(int id);
        Task<bool> ActualizarRelacionesEvento(int id, EventoDTO dto);
    }
}