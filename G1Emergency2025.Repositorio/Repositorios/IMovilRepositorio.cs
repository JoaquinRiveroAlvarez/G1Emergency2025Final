using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface IMovilRepositorio : IRepositorio<Movil>
    {
        Task<Movil?> SelectByCod(string cod);
        Task<List<MovilListadoDTO>> SelectListaMovil();
    }
}