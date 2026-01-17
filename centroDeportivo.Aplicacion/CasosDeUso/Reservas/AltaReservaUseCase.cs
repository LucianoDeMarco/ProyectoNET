using centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.Aplicacion.Validaciones;
using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion;

namespace centroDeportivo.Aplicacion.CasosDeUso.Reservas;

public class AltaReservaUseCase
{
    private readonly IReservaRepository _repo;

    public AltaReservaUseCase (IReservaRepository repo)
    {
        _repo = repo;
    }

    public void Ejecutar(Reserva reserva)
    {
        if (reserva == null)
        {
            throw new ValidacionException("La reserva no puede ser nula.");
        }

        if (!ValidacionesActividad.EsFechaValida(reserva.FechaReserva))
        {
            throw new FechaInvalidaException("La fecha de la reserva no es válida.");
        }

        if (_repo.ExisteInscripcion(reserva.PersonaId, reserva.ActividadId))
        {
            throw new ValidacionException("La persona ya está inscripta en esta actividad.");
        }

        _repo.Guardar(reserva);
    }

}