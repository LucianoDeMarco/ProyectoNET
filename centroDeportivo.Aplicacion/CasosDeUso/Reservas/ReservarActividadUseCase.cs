using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion.Interfaces; // Ojo: Verifica mayúscula 'I'
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.interfaces; // Agregado para 'Estado' y 'Reserva'

namespace centroDeportivo.Aplicacion.CasosDeUso.Reservas;

public class ReservarActividadUseCase
{
    private readonly IReservaRepository _repoReservas;
    private readonly IActividadRepository _repoActividades;
    private readonly IServicioAutorizacion _autorizacion;

    public ReservarActividadUseCase(
        IReservaRepository repoReservas,
        IActividadRepository repoActividades,
        IServicioAutorizacion autorizacion)
    {
        _repoReservas = repoReservas;
        _repoActividades = repoActividades;
        _autorizacion = autorizacion;
    }

    // CORRECCIÓN: El método recibe el OBJETO Usuario y la Reserva
    public void Ejecutar(Usuario usuarioActor, Reserva reserva)
    {
        // 1. Validar Permiso (Opcional: Si el socio siempre puede, podrías quitar esto o dar permiso al socio)
        // Por ahora asumimos que el Usuario Socio tiene este permiso asignado.
        if (!_autorizacion.PoseePermiso(usuarioActor, Permiso.InscripcionAlta))
        {
             // Si esto te da error al probar, avísame y lo quitamos para los socios
            throw new ValidacionException("No tienes permiso para inscribirte.");
        }

        // 2. Validar que la actividad existe
        var actividad = _repoActividades.ObtenerPorId(reserva.ActividadId);
        if (actividad == null)
        {
            throw new ValidacionException("La actividad seleccionada no existe.");
        }

        // 3. Validar Cupo
        int inscriptos = _repoReservas
            .ObtenerTodas() // Nota: Lo ideal sería tener un método ObtenerPorActividad en el repo
            .Count(r => r.ActividadId == reserva.ActividadId &&
                        r.EstadoAsistencia != Estado.Cancelada);

        if (inscriptos >= actividad.CupoMaximo)
        {
            throw new CupoExcedidoException("No hay cupo disponible para la actividad.");
        }

        // 4. Guardar
        _repoReservas.Guardar(reserva);
    }
}