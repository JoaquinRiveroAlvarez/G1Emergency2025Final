using G1Emergency2025.BD.Datos.Entity;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface IHistoricoRepositorio : IRepositorio<Historico>
    {
        Task<Historico?> SelectByFechaEntrada(DateTime FechaEntrada);
        Task<Historico?> SelectByFechaSalida(DateTime FechaSalida);



    }
}