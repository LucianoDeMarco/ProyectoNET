using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion.interfaces;

namespace centroDeportivo.Aplicacion.CasosDeUso.Personas;

public class BajaPersonaUseCase
{
    private readonly IPersonaRepository _repo;

    public BajaPersonaUseCase(IPersonaRepository repo)
    {
        _repo = repo;
    }

    public void Ejecutar(int id)
    {
        var persona = _repo.ObtenerPorId(id);

        if (persona == null)
        {
            throw new ValidacionException("No existe una persona con el Id indicado.");
        }

        _repo.Eliminar(id);
    }
}
