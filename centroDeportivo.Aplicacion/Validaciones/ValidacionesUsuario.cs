namespace centroDeportivo.Aplicacion.Validadores;

public class ValidacionesUsuario
{
    public void Validar(Usuario usuario)
    {
        if (string.IsNullOrWhiteSpace(usuario.Nombre))
            throw new Exception("El nombre es requerido.");
            
        if (string.IsNullOrWhiteSpace(usuario.Apellido))
            throw new Exception("El apellido es requerido.");
            
        if (string.IsNullOrWhiteSpace(usuario.Mail) || !usuario.Mail.Contains("@"))
            throw new Exception("El mail es inválido.");
            
        if (string.IsNullOrWhiteSpace(usuario.Password) || usuario.Password.Length < 4)
            throw new Exception("La contraseña debe tener al menos 4 caracteres.");
    }
}