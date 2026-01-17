namespace centroDeportivo.Aplicacion.Validaciones;
using centroDeportivo.Aplicacion;

public static class ValidacionesActividad
{
    public static bool EsValida(ActividadDeportiva actividad)
    {
        bool tieneNombre = !string.IsNullOrWhiteSpace(actividad.Nombre);
        bool tieneCupo = actividad.CupoMaximo > 0;
        
        bool responsableValido = actividad.Responsable != null && 
                                 !string.IsNullOrWhiteSpace(actividad.Responsable.Nombre);

        bool diasValidos = !string.IsNullOrWhiteSpace(actividad.DiasDisponibles);

        return tieneNombre && tieneCupo && responsableValido && diasValidos;
    }

    public static bool EsFechaValida(DateTime fecha)
    {
        return fecha.Date >= DateTime.Today; 
    }
}