using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.interfaces; 
using Microsoft.EntityFrameworkCore;

namespace centroDeportivo.Repositorios;

public class ActividadRepositorioDB : IActividadRepository
{
    // Eliminamos el constructor y variable privada para usar el patrón "using"
    // igual que en tu UsuarioRepositorio, para evitar problemas de configuración.

    public List<ActividadDeportiva> ObtenerTodas()
    {
        using var db = new CentroDeportivoContext();
        
        // CORREGIDO: Agregamos .Include para traer los datos del Responsable (Profe)
        // Sin esto, act.Responsable sería null en la pantalla.
        return db.Actividades
                 .Include(a => a.Responsable) 
                 .ToList();
    }

    public ActividadDeportiva? ObtenerPorId(int id)
    {
        using var db = new CentroDeportivoContext();
        
        return db.Actividades
                 .Include(a => a.Responsable) // También aquí por si se usa en detalle
                 .FirstOrDefault(a => a.Id == id);
    }

    public void Guardar(ActividadDeportiva actividad)
    {
        using var db = new CentroDeportivoContext();
        
        // Si el Responsable ya existe en la DB, evitamos que intente crearlo de nuevo
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