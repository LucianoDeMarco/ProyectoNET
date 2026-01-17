using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Aplicacion.interfaces;

namespace centroDeportivo.Aplicacion.CasosDeUso.Reservas;

public class CancelarReservaUseCase
{
    private readonly IReservaRepository _repo;
    private readonly IServicioAutorizacion _autorizacion;

    public CancelarReservaUseCase(
        IReservaRepository repo,
        IServicioAutorizacion autorizacion)
    {
        _repo = repo;
        _autorizacion = autorizacion;
    }

    // AHORA RECIBE EL ID (int)
    public void Ejecutar(Usuario usuarioActor, int idReserva)
    {
        // 1. Primero buscamos la reserva en la BD
        var reserva = _repo.ObtenerPorId(idReserva);
        if (reserva == null)
        {
            throw new ValidacionException("La reserva no existe.");
        }

        // 2. VALIDACIÓN DE PERMISOS INTELIGENTE
        // ¿Es el dueño de la reserva?
        bool esElDueño = (reserva.PersonaId == usuarioActor.Id);
        
        // ¿O es un administrador con permisos de borrar cualquiera?
        bool esAdmin = _autorizacion.PoseePermiso(usuarioActor, Permiso.InscripcionBaja);

        // Si no es ni el dueño ni el admin, tiramos error
        if (!esElDueño && !esAdmin)
        {
            throw new ValidacionException("No tienes permiso para cancelar esta reserva.");
        }

        // 3. Ejecutar la baja
        reserva.EstadoAsistencia = Estado.Cancelada;
        _repo.Modificar(reserva);
    }
}