using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface IRolRepositorio : IRepositorio<Rol>
    {
        Task<Rol?> SelectByCod(string cod);
        Task<List<RolListadoDTO>> SelectListaRol();
    }
}