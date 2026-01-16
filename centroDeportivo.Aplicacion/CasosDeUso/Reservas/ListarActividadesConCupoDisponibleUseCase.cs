using centroDeportivo.Aplicacion.interfaces;

namespace centroDeportivo.Aplicacion.CasosDeUso.Actividades;

public class ListarActividadesConCupoDisponibleUseCase
{
    private readonly IActividadRepository _actividadRepo;
    private readonly IReservaRepository _reservaRepo;

    public ListarActividadesConCupoDisponibleUseCase(
        IActividadRepository actividadRepo,
        IReservaRepository reservaRepo)
    {
        _actividadRepo = actividadRepo;
        _reservaRepo = reservaRepo;
    }

    public List<ActividadDeportiva> Ejecutar()
    {
        var actividades = _actividadRepo.ObtenerTodas();

        return actividades
            .Where(a => _reservaRepo.ContarReservasPorActividad(a.Id) < a.CupoMaximo)
            .ToList();
    }
}
