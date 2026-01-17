namespace centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion;
public interface IUsuarioRepositorio
{
    void AgregarUsuario(Usuario usuario);
    List<Usuario> ListarUsuarios();
    Usuario? ObtenerUsuarioPorMail(string mail); // Útil para el login
    void EliminarUsuario(int id);
    void ModificarUsuario(Usuario usuario);
}