using centroDeportivo.Aplicacion.Seguridad;

namespace centroDeportivo.Aplicacion;

public class Usuario
{
    public int Id { get; set; }
    
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = ""; 
    public string Mail { get; set; } = "";
    public string Password { get; set; } = "";
    public List<Permiso>? ListaPermisos {get; set;} = new List<Permiso>();
}