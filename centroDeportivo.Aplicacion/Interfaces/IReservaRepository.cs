namespace centroDeportivo.Aplicacion.interfaces;

public interface IReservaRepository
{
    // Lectura
    List<Reserva> ObtenerTodas();
    Reserva? ObtenerPorId(int id);

    // Escritura
    void Guardar(Reserva reserva);   // Alta
    void Modificar(Reserva reserva);
    void Eliminar(int id);           // Baja

    // Validaciones
    bool ExisteInscripcion(int personaId, int actividadId);
    int ContarReservasPorActividad(int actividadId);
}
