namespace centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Aplicacion;

public interface IServicioAutorizacion
{
    bool PoseePermiso(Usuario usuario, Permiso permisoRequerido);
}