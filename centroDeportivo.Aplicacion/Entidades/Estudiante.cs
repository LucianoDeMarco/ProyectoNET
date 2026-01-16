namespace centroDeportivo.Aplicacion.Entidades;

public class Estudiante : Persona
{
    public int NroAlumno { get; set; }
    public string Carrera { get; set; } = "";
}