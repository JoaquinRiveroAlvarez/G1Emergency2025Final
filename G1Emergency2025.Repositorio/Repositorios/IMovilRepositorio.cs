using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;
using G1Emergency2025.Shared.Enum;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface IMovilRepositorio : IRepositorio<Movil>
    {
        Task AsociarEvento(int movilId, int eventoId);
        Task<MovilListadoDTO?> SelectMovilPorId(int id);
        Task<List<MovilListadoDTO>> SelectListaMovil();
        Task<MovilConEventosDTO> SelectListaMovilPorPatente(string patente);
        //Task<List<MovilListadoDTO>> SelectListaMovilPorEvento(int eventoId);
        Task<List<MovilListadoDTO>> SelectListaMovilPorDisponibilidad(DisponibilidadMovil disponibilidadMovil);
        Task<List<MovilConEventosDTO>> SelectListaMovilPorTipoMovil(string tipoMovil);
        Task<List<MovilConEventosDTO>> SelectListaMovilPorCodTipoMovil(string codigo);
        //Task<List<MovilesPorDisponibilidadDTO>> SelectListaMovilAgrupada();
    }
}