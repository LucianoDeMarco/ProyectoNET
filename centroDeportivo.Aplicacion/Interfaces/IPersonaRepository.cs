namespace centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.Aplicacion;
public interface IPersonaRepository
{
    Persona? ObtenerPorDni(int dni);
    Persona? ObtenerPorId(int id);
    List<Persona> ObtenerTodas();

    void Guardar(Persona persona);      // Alta
    void Modificar(Persona persona);    // Modificación
    void Eliminar(int id); 
}