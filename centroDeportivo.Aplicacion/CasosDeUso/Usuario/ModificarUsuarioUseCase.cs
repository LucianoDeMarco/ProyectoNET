namespace centroDeportivo.Aplicacion.CasosDeUso;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Aplicacion.Validadores;
using centroDeportivo.Aplicacion;
public class ModificarUsuarioUseCase
{
    private readonly IUsuarioRepositorio _repo;
    private readonly IServicioAutorizacion _auth;
    private readonly ValidacionesUsuario _validador;
    private readonly ServicioHash _hashService;

    public ModificarUsuarioUseCase(IUsuarioRepositorio repo, IServicioAutorizacion auth, ValidacionesUsuario validador, ServicioHash hashService)
    {
        _repo = repo;
        _auth = auth;
        _validador = validador;
        _hashService = hashService;
    }


       public void Ejecutar(Usuario ejecutante, Usuario usuarioModificado, bool esResetDePassword = false)
        {
            // Permitir si es Admin O si se está editando a sí mismo
            bool esSimesmo = ejecutante.Id == usuarioModificado.Id;
            bool tienePermisoAdmin = _auth.PoseePermiso(ejecutante, Permiso.UsuarioModificacion);

            if (!esSimesmo && !tienePermisoAdmin)
            {
                throw new Exception("No tienes permiso para modificar a otros usuarios.");
            }

        if (esResetDePassword)
        {
            usuarioModificado.Password = _hashService.CalcularHash(usuarioModificado.Password);
        }

            _validador.Validar(usuarioModificado);
            _repo.ModificarUsuario(usuarioModificado);
        }
}