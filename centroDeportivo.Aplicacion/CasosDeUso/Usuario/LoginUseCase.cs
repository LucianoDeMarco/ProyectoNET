using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;

public class LoginUseCase
{
    private readonly IUsuarioRepositorio _repo;
    private readonly ServicioHash _hashService;

    public LoginUseCase(IUsuarioRepositorio repo, ServicioHash hashService){
        _repo = repo;
        _hashService = hashService;
    }
    public Usuario Ejecutar(string mail, string password)
    {
        var usuario = _repo.ObtenerUsuarioPorMail(mail);
    
        if (usuario == null)
            throw new Exception("El correo electrónico no está registrado.");

        string hashIngresado = _hashService.CalcularHash(password);

        if (usuario.Password != hashIngresado)
            throw new Exception("La contraseña es incorrecta.");

        return usuario;
    }
}