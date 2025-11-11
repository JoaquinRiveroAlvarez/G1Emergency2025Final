
using G1Emergency2025.Shared.Enum;

namespace G1Emergency2025.BD.Datos
{
    public interface IEntityBase
    {
        int Id { get; set; }
        string Observacion { get; set; }
        EnumEstadoRegistro EstadoRegistro { get; set; }
    }
}