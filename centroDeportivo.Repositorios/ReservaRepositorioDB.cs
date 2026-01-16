using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.interfaces;
using Microsoft.EntityFrameworkCore;

namespace centroDeportivo.Repositorios;

public class ReservaRepositorioDB : IReservaRepository
{
    private readonly CentroDeportivoContext _context;

    public ReservaRepositorioDB(CentroDeportivoContext context)
    {
        _context = context;
    }

    public List<Reserva> ObtenerTodas()
    {
        return _context.Reservas.ToList();
    }

    public Reserva? ObtenerPorId(int id)
    {
        return _context.Reservas.Find(id);
    }

    public void Guardar(Reserva reserva)
    {
        _context.Reservas.Add(reserva);
        _context.SaveChanges();
    }

    public void Modificar(Reserva reserva)
    {
        _context.Reservas.Update(reserva);
        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        var r = ObtenerPorId(id);
        if (r != null)
        {
            _context.Reservas.Remove(r);
            _context.SaveChanges();
        }
    }

    public bool ExisteInscripcion(int personaId, int actividadId)
    {
        // Consultamos directamente en la DB si existe la reserva activa
        return _context.Reservas.Any(r =>
            r.PersonaId == personaId &&
            r.ActividadId == actividadId &&
            r.EstadoAsistencia != Estado.Cancelada);
    }

    public int ContarReservasPorActividad(int actividadId)
    {
        // Contamos las reservas que no estén canceladas para esa actividad
        return _context.Reservas.Count(r =>
            r.ActividadId == actividadId &&
            r.EstadoAsistencia != Estado.Cancelada);
    }
}