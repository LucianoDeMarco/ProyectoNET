namespace centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion;
public class ServicioAutorizacion : IServicioAutorizacion
{
    public bool PoseePermiso(Usuario usuario, Permiso permisoRequerido)
    {
        if (usuario == null) return false;

        // 2. Si tiene el permiso de "Administrador", puede hacer TODO
        if (usuario.ListaPermisos != null && usuario.ListaPermisos.Contains(Permiso.Administrador))
        {
            return true;
        }

        // 3. Si no es admin, verificamos si tiene el permiso específico en su lista
        return usuario.ListaPermisos != null && usuario.ListaPermisos.Contains(permisoRequerido);
    }
}