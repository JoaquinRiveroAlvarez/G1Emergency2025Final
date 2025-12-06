using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface IEventoRepositorio : IRepositorio<Evento>
    {
        Task<EventoDiagPresuntivoListadoDTO?> SelectPorId(int eventoId);
        Task<List<EventoListadoDTO>> SelectListaEventoReciente();
        Task<List<EventoListadoDTO>> SelectListaEvento();
        Task<List<EventoDiagPresuntivoListadoDTO>> SelectPorTipoEstado(int estadoEventoId);
        Task<List<EventoDiagPresuntivoListadoDTO>> SelectPorNombrePaciente(string nombrePaciente);
        Task<List<EventoDiagPresuntivoListadoDTO>> SelectPorHistoriaClinicaPaciente(string historiaClinica);
        Task<EventoPacienteDiagPresuntivoResumenDTO?> SelectEventoConPacientePorId(int id);
        Task<EventoDiagPresuntivoListadoDTO?> SelectPorCod(string cod);
        Task<List<EventoDiagPresuntivoListadoDTO>> SelectPorFechaFlexible(int? anio = null, int? mes = null, int? dia = null, int? hora = null);
        Task<List<EventoDiagPresuntivoListadoDTO>> SelectListaEventoCompleto();
        Task<List<EventoDiagPresuntivoListadoDTO>> SelectListaEventoRecienteCompleto();
        Task<int> InsertarEvento(EventoDTO dto);
        Task<int> InsertarEventoPaciente(EventoCrearDTO dto);
        Task<bool> ActualizarEvento(int id, EventoDTO dto);
        Task<bool> ActualizarEventoPaciente(int id, EventoCrearDTO dto);
        Task<bool> ActualizarEstadoEvento(int id, int estadoId);
        Task<bool> DeleteEvento(int id);
        Task<bool> ActualizarRelacionesEvento(int id, EventoDTO dto);
    }
}