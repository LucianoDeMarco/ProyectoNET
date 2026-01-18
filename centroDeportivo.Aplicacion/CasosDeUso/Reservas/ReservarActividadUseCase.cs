using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.interfaces;

namespace centroDeportivo.Aplicacion.CasosDeUso.Reservas;

public class ReservarActividadUseCase
{
    private readonly IReservaRepository _reservaRepo;
    private readonly IActividadRepository _actividadRepo;
    private readonly IServicioAutorizacion _autorizacion;

    public ReservarActividadUseCase(
        IReservaRepository reservaRepo, 
        IActividadRepository actividadRepo,
        IServicioAutorizacion autorizacion)
    {
        _reservaRepo = reservaRepo;
        _actividadRepo = actividadRepo;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Usuario usuario, int idActividad)
    {


        // 1. Buscar la Actividad
        var actividad = _actividadRepo.ObtenerPorId(idActividad);
        if (actividad == null)
        {
            throw new ValidacionException("La actividad indicada no existe.");
        }

        // 2. Evitar doble inscripción
        // Traemos todas las reservas y buscamos si este usuario ya tiene una para esta actividad
        // que NO esté cancelada.
        var reservasExistentes = _reservaRepo.ObtenerTodas();
        bool yaEstaInscripto = reservasExistentes.Any(r => 
            r.ActividadId == idActividad && 
            r.PersonaId == usuario.Id && 
            r.EstadoAsistencia != Estado.Cancelada);

        if (yaEstaInscripto)
        {
            throw new ValidacionException("¡Ya estás inscripto en esta actividad!");
        }

        // 4. Validar Cupo
        // Contamos cuántos inscriptos activos hay
        int inscriptosActuales = reservasExistentes.Count(r => 
            r.ActividadId == idActividad && 
            r.EstadoAsistencia != Estado.Cancelada);

        if (inscriptosActuales >= actividad.CupoMaximo)
        {
            throw new ValidacionException("Lo sentimos, no hay cupos disponibles.");
        }

        // 5. Crear y Guardar la Reserva
        var nuevaReserva = new Reserva
        {
            FechaReserva = DateTime.Now,
            PersonaId = usuario.Id,
            ActividadId = idActividad,
            EstadoAsistencia = Estado.Pendiente
        };

        _reservaRepo.Guardar(nuevaReserva);
    }
}