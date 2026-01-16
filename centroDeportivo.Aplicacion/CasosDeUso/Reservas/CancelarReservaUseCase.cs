using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;

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

    public void Ejecutar(Usuario idUsuario, int idReserva)
    {
        if (!_autorizacion.PoseePermiso(idUsuario, Permiso.InscripcionBaja))
        {
            throw new ValidacionException("El usuario no tiene permiso para cancelar inscripciones.");
        }

        var reserva = _repo.ObtenerPorId(idReserva);
        if (reserva == null)
        {
            throw new ValidacionException("La reserva no existe.");
        }

        reserva.EstadoAsistencia = Estado.Cancelada;
        _repo.Modificar(reserva);
    }
}
