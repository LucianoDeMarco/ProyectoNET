namespace centroDeportivo.Aplicacion;

public abstract class Persona
{
    public int Id { get; set; }
    public int NroCarnet { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Direccion { get; set; } = "";
    public string Facultad { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string Mail { get; set; } = "";

    public Persona() 
    {    
    }
}