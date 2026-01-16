using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.Interfaces;

public class LoginUseCase
{
    private readonly IUsuarioRepositorio _repo;

    public LoginUseCase(IUsuarioRepositorio repo) => _repo = repo;

    public Usuario Ejecutar(string mail, string password)
    {
        var usuario = _repo.ObtenerUsuarioPorMail(mail);
    
        if (usuario == null)
            throw new Exception("El correo electrónico no está registrado.");

        if (usuario.Password != password)
            throw new Exception("La contraseña es incorrecta.");

        return usuario;
    }
}