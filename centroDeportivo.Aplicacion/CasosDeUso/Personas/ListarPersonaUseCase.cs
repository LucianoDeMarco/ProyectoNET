using centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.Aplicacion;

namespace centroDeportivo.Aplicacion.CasosDeUso.Personas;

public class ListarPersonasUseCase
{
    private readonly IPersonaRepository _repo;

    public ListarPersonasUseCase(IPersonaRepository repo)
    {
        _repo = repo;
    }

    public List<Persona> Ejecutar()
    {
        return _repo.ObtenerTodas();
    }
}
