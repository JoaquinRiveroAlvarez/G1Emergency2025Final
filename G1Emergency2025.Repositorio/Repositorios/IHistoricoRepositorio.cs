using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface IHistoricoRepositorio : IRepositorio<Historico>
    {
        Task<Historico?> SelectByFechaEntrada(DateTime FechaEntrada);
        Task<Historico?> SelectByFechaSalida(DateTime FechaSalida);
        Task<List<HistoricoListadoDTO>> SelectListaHistorico();
        Task RegistrarEntrada(int tripulanteId, int movilId);
        Task RegistrarSalida(int tripulanteId, int movilId);
    }
}