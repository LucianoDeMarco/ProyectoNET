namespace centroDeportivo.Aplicacion.CasosDeUso;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Aplicacion;
public class EliminarUsuarioUseCase
{
    private readonly IUsuarioRepositorio _repo;
    private readonly IServicioAutorizacion _auth;

    public EliminarUsuarioUseCase(IUsuarioRepositorio repo, IServicioAutorizacion auth)
    {
        _repo = repo;
        _auth = auth;
    }

    public void Ejecutar(Usuario ejecutante, int idUsuarioAEliminar)
    {
        if (!_auth.PoseePermiso(ejecutante, Permiso.UsuarioBaja))
        {
            throw new Exception("No tienes autoridad para eliminar usuarios.");
        }

        if (ejecutante.Id == idUsuarioAEliminar)
        {
            throw new Exception("No puedes eliminar tu propia cuenta de usuario.");
        }

        _repo.EliminarUsuario(idUsuarioAEliminar);
    }
}