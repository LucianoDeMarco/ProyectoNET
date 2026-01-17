namespace centroDeportivo.Aplicacion;

public class ActividadDeportiva
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string DiasDisponibles { get; set; } = ""; 
    public int CupoMaximo { get; set; }

    public Persona? Responsable { get; set; }
    
}