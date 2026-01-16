using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;

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

    public void Ejecutar(Usuario idUsuario, Reserva reserva)
    {
        if (!_autorizacion.PoseePermiso(idUsuario, Permiso.InscripcionAlta))
        {
            throw new ValidacionException("El usuario no tiene permiso para registrar inscripciones.");
        }

        var actividad = _repoActividades.ObtenerPorId(reserva.ActividadId);
        if (actividad == null)
        {
            throw new ValidacionException("La actividad no existe.");
        }

        int inscriptos = _repoReservas
            .ObtenerTodas()
            .Count(r => r.ActividadId == reserva.ActividadId &&
                        r.EstadoAsistencia != Estado.Cancelada);

        if (inscriptos >= actividad.CupoMaximo)
        {
            throw new CupoExcedidoException("No hay cupo disponible para la actividad.");
        }

        _repoReservas.Guardar(reserva);
    }
}
