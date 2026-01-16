using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;

namespace centroDeportivo.Aplicacion.CasosDeUso.Actividades;

public class BajaActividadUseCase
{
    private readonly IActividadRepository _repo;
    private readonly IServicioAutorizacion _autorizacion;

    public BajaActividadUseCase(IActividadRepository repo, IServicioAutorizacion autorizacion)
    {
        _repo = repo;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Usuario idUsuario, int idActividad)
    {
        if (!_autorizacion.PoseePermiso(idUsuario, Permiso.ActividadBaja))
        {
            throw new ValidacionException("El usuario no tiene permiso para eliminar actividades.");
        }

        var actividad = _repo.ObtenerPorId(idActividad);
        if (actividad == null)
        {
            throw new ValidacionException("No existe una actividad con el Id indicado.");
        }

        _repo.Eliminar(idActividad);
    }
}
