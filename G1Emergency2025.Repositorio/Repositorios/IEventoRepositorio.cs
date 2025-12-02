using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface IEventoRepositorio : IRepositorio<Evento>
    {
        Task<List<EventoListadoDTO>> SelectListaEventoReciente();
        Task<List<EventoListadoDTO>> SelectListaEvento();
        Task<List<EventoListadoDTO>> SelectPorTipoEstado(int estadoEventoId);
        Task<List<EventoListadoDTO>> SelectPorNombrePaciente(string nombrePaciente);
        Task<EventoListadoDTO?> SelectPorCod(string cod);
        Task<List<EventoListadoDTO>> SelectPorFechaFlexible(int? anio = null, int? mes = null, int? dia = null, int? hora = null);
        Task<List<EventoListadoDTO>> SelectListaEventoConDisponibilidad();
        Task<int> InsertarEvento(EventoDTO dto);
        Task<int> InsertarEventoPaciente(EventoCrearDTO dto);
        Task<bool> ActualizarEvento(int id, EventoDTO dto);
        Task<bool> ActualizarEventoPaciente(int id, EventoCrearDTO dto);
        Task<bool> ActualizarEstadoEvento(int id, int estadoId);
        Task<bool> DeleteEvento(int id);
        Task<bool> ActualizarRelacionesEvento(int id, EventoDTO dto);
    }
}