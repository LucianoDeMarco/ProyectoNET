namespace centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;

public interface IServicioAutorizacion
{
    bool PoseePermiso(Usuario usuario, Permiso permisoRequerido);
}