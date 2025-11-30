using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface ILugarHechoRepositorio : IRepositorio<LugarHecho>
    {
        Task<LugarHecho?> SelectByCod(string cod);
        Task<List<LugarHechoListadoDTO>> SelectListaLugarHecho();
        Task AsociarEvento(int lugarhechoId, int eventoId);
    }
}