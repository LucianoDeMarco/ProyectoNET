using centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.Aplicacion;

namespace centroDeportivo.Aplicacion.Validaciones;
public static class ValidacionesResponsable
{
    public static bool EsValido(Persona p, IPersonaRepository repo)
    {
        bool datosBasicosValidos = !string.IsNullOrWhiteSpace(p.Nombre) && 
                                   !string.IsNullOrWhiteSpace(p.Mail);

        if (!datosBasicosValidos) return false;

        bool dniRepetido = repo.ObtenerPorDni(p.NroCarnet) != null;

        return !dniRepetido; 
    }
}