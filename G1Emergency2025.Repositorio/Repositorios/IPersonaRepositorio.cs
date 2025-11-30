using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Repositorio.Repositorios;
using G1Emergency2025.Shared.DTO;

namespace G1Emergency2025.Repositorio.IRepositorios
{
    public interface IPersonaRepositorio : IRepositorio<Persona>
    {
        Task<Persona?> SelectByDni(string dni);
        Task<List<PersonaListadoDTO>> SelectListaPersona();
    }
}