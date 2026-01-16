using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.Aplicacion.Validaciones;
using centroDeportivo.Aplicacion.Interfaces;

namespace centroDeportivo.Aplicacion.CasosDeUso.Actividades;

public class AltaActividadUseCase
{
    private readonly IActividadRepository _repo;
    private readonly IServicioAutorizacion _autorizacion;

    public AltaActividadUseCase(
        IActividadRepository repo,
        IServicioAutorizacion autorizacion)
    {
        _repo = repo;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Usuario idUsuario, ActividadDeportiva actividad)
    {
        if (!_autorizacion.PoseePermiso(idUsuario, Permiso.ActividadAlta))
        {
            throw new ValidacionException("El usuario no tiene permiso para crear actividades.");
        }

        if (!ValidacionesActividad.EsValida(actividad))
        {
            throw new ValidacionException("Datos inválidos para la actividad.");
        }

        _repo.Guardar(actividad);
    }
}
