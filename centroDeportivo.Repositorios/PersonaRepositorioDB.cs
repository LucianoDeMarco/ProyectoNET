using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.interfaces;
using Microsoft.EntityFrameworkCore;

namespace centroDeportivo.Repositorios;

public class PersonaRepositorioDB : IPersonaRepository
{
    private readonly CentroDeportivoContext _context;

    public PersonaRepositorioDB(CentroDeportivoContext context)
    {
        _context = context;
    }

    public List<Persona> ObtenerTodas()
    {
        return _context.Personas.ToList();
    }

    public Persona? ObtenerPorId(int id)
    {
        return _context.Personas.Find(id);
    }

    public Persona? ObtenerPorDni(int dni)
    {
        // En EF usamos FirstOrDefault directamente sobre el DbSet
        return _context.Personas.FirstOrDefault(p => p.NroCarnet == dni);
    }

    public void Guardar(Persona persona)
    {
        _context.Personas.Add(persona);
        _context.SaveChanges();
    }

    public void Modificar(Persona persona)
    {
        _context.Personas.Update(persona);
        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        var persona = ObtenerPorId(id);
        if (persona != null)
        {
            _context.Personas.Remove(persona);
            _context.SaveChanges();
        }
    }
}