using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Aplicacion.Validaciones;

namespace centroDeportivo.Aplicacion.CasosDeUso.Actividades;

public class ModificarActividadUseCase
{
    private readonly IActividadRepository _repo;
    private readonly IServicioAutorizacion _autorizacion;

    public ModificarActividadUseCase(IActividadRepository repo, IServicioAutorizacion autorizacion)
    {
        _repo = repo;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Usuario idUsuario, ActividadDeportiva actividad)
    {

        if (!_autorizacion.PoseePermiso(idUsuario, Permiso.ActividadModificacion))
        {
            throw new ValidacionException("El usuario no tiene permiso para crear actividades.");
        }

        var existente = _repo.ObtenerPorId(actividad.Id);

        if (existente == null)
        {
            throw new ValidacionException("No existe una actividad con el Id indicado.");
        }

        if (!ValidacionesActividad.EsValida(actividad))
        {
            throw new ValidacionException("Datos inválidos para la actividad.");
        }

        _repo.Modificar(actividad);
    }
}
