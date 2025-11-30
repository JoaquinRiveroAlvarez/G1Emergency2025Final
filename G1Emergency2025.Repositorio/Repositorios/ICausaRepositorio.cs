using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface ICausaRepositorio : IRepositorio<Causa>
    {
        Task<Causa?> SelectByCod(string cod);
        Task<List<CausaListadoDTO>> SelectListaCausa();
    }
}