using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.interfaces;
using Microsoft.EntityFrameworkCore;

namespace centroDeportivo.Repositorios;

public class ActividadRepositorioDB : IActividadRepository
{
    private readonly CentroDeportivoContext _context;

    public ActividadRepositorioDB(CentroDeportivoContext context)
    {
        _context = context;
    }

    public List<ActividadDeportiva> ObtenerTodas()
    {
        // Retornamos la lista desde la base de datos
        return _context.Actividades.ToList();
    }

    public ActividadDeportiva? ObtenerPorId(int id)
    {
        // Buscamos por la clave primaria
        return _context.Actividades.Find(id);
    }

    public void Guardar(ActividadDeportiva actividad)
    {
        _context.Actividades.Add(actividad);
        _context.SaveChanges(); // Persiste en el archivo .db
    }

    public void Modificar(ActividadDeportiva actividad)
    {
        // Marcamos la entidad como modificada y guardamos
        _context.Actividades.Update(actividad);
        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        var act = ObtenerPorId(id);
        if (act != null)
        {
            _context.Actividades.Remove(act);
            _context.SaveChanges();
        }
    }
}