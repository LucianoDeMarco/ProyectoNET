namespace centroDeportivo.Aplicacion.CasosDeUso;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Aplicacion.Validadores;

public class ModificarUsuarioUseCase
{
    private readonly IUsuarioRepositorio _repo;
    private readonly IServicioAutorizacion _auth;
    private readonly ValidacionesUsuario _validador;

    public ModificarUsuarioUseCase(IUsuarioRepositorio repo, IServicioAutorizacion auth, ValidacionesUsuario validador)
    {
        _repo = repo;
        _auth = auth;
        _validador = validador;
    }


       public void Ejecutar(Usuario ejecutante, Usuario usuarioModificado)
        {
            // Permitir si es Admin O si se está editando a sí mismo
            bool esSimesmo = ejecutante.Id == usuarioModificado.Id;
            bool tienePermisoAdmin = _auth.PoseePermiso(ejecutante, Permiso.UsuarioModificacion);

            if (!esSimesmo && !tienePermisoAdmin)
            {
                throw new Exception("No tienes permiso para modificar a otros usuarios.");
            }

            _validador.Validar(usuarioModificado);
            _repo.ModificarUsuario(usuarioModificado);
        }
}