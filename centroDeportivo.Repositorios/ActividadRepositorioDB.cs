using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.interfaces; 
using Microsoft.EntityFrameworkCore;

namespace centroDeportivo.Repositorios;

public class ActividadRepositorioDB : IActividadRepository
{

    public List<ActividadDeportiva> ObtenerTodas()
    {
        using var db = new CentroDeportivoContext();
        
        return db.Actividades
                 .Include(a => a.Responsable) 
                 .ToList();
    }

    public ActividadDeportiva? ObtenerPorId(int id)
    {
        using var db = new CentroDeportivoContext();
        
        return db.Actividades
                 .Include(a => a.Responsable) 
                 .FirstOrDefault(a => a.Id == id);
    }

    public void Guardar(ActividadDeportiva actividad)
    {
        using var db = new CentroDeportivoContext();
        
        if (actividad.Responsable != null)
        {
            db.Entry(actividad.Responsable).State = EntityState.Unchanged;
        }

        db.Actividades.Add(actividad);
        db.SaveChanges();
    }

    public void Modificar(ActividadDeportiva actividad)
    {
        using var db = new CentroDeportivoContext();
        db.Actividades.Update(actividad);
        db.SaveChanges();
    }

    public void Eliminar(int id)
    {
        using var db = new CentroDeportivoContext();
        var act = db.Actividades.Find(id);
        if (act != null)
        {
            db.Actividades.Remove(act);
            db.SaveChanges();
        }
    }
}