namespace centroDeportivo.Aplicacion.Entidades;

public class Docente : Persona
{
    public string Matricula { get; set; } = "";
    public DateTime AnioIngreso { get; set; }
}