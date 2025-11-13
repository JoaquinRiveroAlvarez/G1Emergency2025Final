using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface IPacienteRepositorio : IRepositorio<Paciente>
    {
        Task<Paciente?> SelectByObraSocial(string cod);
        Task<List<PacienteResumenDTO>> SelectListaPaciente();
        Task AsociarEvento(int pacienteId, int eventoId);
        Task<int> CrearPacienteConPersona(Persona persona, Paciente paciente, int eventoId);
        Task<bool> UpdatePacienteConEventos(int id, PacienteActualizarDTO dto);
        Task<Paciente?> ObtenerConPersonaAsync(int id);
    }
}