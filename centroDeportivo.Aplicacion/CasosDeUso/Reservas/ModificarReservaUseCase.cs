using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Aplicacion.Validaciones;

namespace centroDeportivo.Aplicacion.CasosDeUso.Reservas;

public class ModificarReservaUseCase
{
    private readonly IReservaRepository _repo;
    private readonly IServicioAutorizacion _autorizacion;

    public ModificarReservaUseCase(
        IReservaRepository repo,
        IServicioAutorizacion autorizacion)
    {
        _repo = repo;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Usuario idUsuario, Reserva reserva)
    {
       
        if (!_autorizacion.PoseePermiso(idUsuario, Permiso.InscripcionModificacion))
        {
            throw new ValidacionException("El usuario no tiene permiso para modificar inscripciones.");
        }

       
        var existente = _repo.ObtenerPorId(reserva.Id);
        if (existente == null)
        {
            throw new ValidacionException("La reserva no existe.");
        }

      
        if (!ValidacionesActividad.EsFechaValida(reserva.FechaReserva))
        {
            throw new FechaInvalidaException("La fecha de la reserva es inválida.");
        }

        
        existente.FechaReserva = reserva.FechaReserva;
        existente.EstadoAsistencia = reserva.EstadoAsistencia;

        _repo.Modificar(existente);
    }
}
