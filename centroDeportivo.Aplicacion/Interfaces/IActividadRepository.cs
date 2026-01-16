namespace centroDeportivo.Aplicacion.interfaces;

public interface IActividadRepository
{
    // Lectura
    ActividadDeportiva? ObtenerPorId(int id);
    List<ActividadDeportiva> ObtenerTodas();

    // Escritura
    void Guardar(ActividadDeportiva actividad);
    void Modificar(ActividadDeportiva actividad);
    void Eliminar(int id);
}
