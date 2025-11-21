using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Task<Usuario?> SelectByCod(string cod);
        Task<List<UsuarioListadoDTO>> SelectListaUsuario();
        Task AsociarEvento(int usuarioId, int eventoId);
    }
}