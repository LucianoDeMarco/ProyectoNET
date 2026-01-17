using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.interfaces;

namespace centroDeportivo.Aplicacion.CasosDeUso.Actividades;

public class ListarActividadesUseCase
{
    private readonly IActividadRepository _repo;

    public List<ActividadDeportiva> Ejecutar()
    {
        return _repo.ObtenerTodas();
    }

    public ListarActividadesUseCase(IActividadRepository repo)
    {
        _repo = repo;
    }
}
