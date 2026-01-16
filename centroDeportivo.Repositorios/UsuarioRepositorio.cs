using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.Interfaces;

namespace centroDeportivo.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    public void AgregarUsuario(Usuario usuario)
    {
        if (usuario == null) throw new ArgumentNullException(nameof(usuario));
        
        using var db = new CentroDeportivoContext();
        db.Usuarios.Add(usuario);
        db.SaveChanges();
    }

    public List<Usuario> ListarUsuarios()
    {
        using var db = new CentroDeportivoContext();
        return db.Usuarios.ToList();
    }

    public Usuario? ObtenerUsuarioPorMail(string mail)
    {
        using var db = new CentroDeportivoContext();
        return db.Usuarios.FirstOrDefault(u => u.Mail == mail);
    }

    public void EliminarUsuario(int id)
    {
        using var db = new CentroDeportivoContext();
        var u = db.Usuarios.Find(id);
        if (u != null)
        {
            db.Usuarios.Remove(u);
            db.SaveChanges();
        }
    }

    public void ModificarUsuario(Usuario usuario)
    {
        using var db = new CentroDeportivoContext();
        db.Usuarios.Update(usuario);
        db.SaveChanges();
    }
}