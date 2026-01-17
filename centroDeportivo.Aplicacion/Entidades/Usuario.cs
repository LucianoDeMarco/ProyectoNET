using centroDeportivo.Aplicacion.Seguridad;
using System.ComponentModel.DataAnnotations;

namespace centroDeportivo.Aplicacion;

public class Usuario
{
    public int Id { get; set; }
    
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = ""; 
    public string Mail { get; set; } = "";

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [MinLength(4, ErrorMessage = "La contraseña debe tener al menos 4 caracteres")]
    public string Password { get; set; } = string.Empty;
    public List<Permiso>? ListaPermisos {get; set;} = new List<Permiso>();
}