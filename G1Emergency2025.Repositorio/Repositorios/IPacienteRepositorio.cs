using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface IPacienteRepositorio : IRepositorio<Paciente>
    {
        Task<PacienteResumenDTO?> SelectByObraSocial(string cod);
        Task<List<PacienteResumenDTO>> SelectListaPaciente();
        Task<List<PacienteResumenDTO>> SelectListaPacienteConPersonaYEvento();
        Task AsociarEvento(int pacienteId, int eventoId, string diagnosticoPresuntivo);
        Task<int> CrearPacienteConPersona(PacienteCrearDTO dto);
        Task<bool> UpdatePacienteConEventos(int id, PacienteActualizarDTO dto);
        Task<PacienteResumenDTO?> SelectNombrePacienteConPersonaYEvento(string nombre);
        Task<PacienteResumenDTO?> SelectHistoriaClinicaPacienteConPersonaYEvento(string nombre);
        Task<PacienteResumenDTO?> SelectDNIPacienteConPersonaYEvento(string cod);
    }
}