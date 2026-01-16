using centroDeportivo.Aplicacion.interfaces;

namespace centroDeportivo.Aplicacion.CasosDeUso.Reservas;

public class ListarReservasActivasUseCase
{
    private readonly IReservaRepository _repo;

    public ListarReservasActivasUseCase(IReservaRepository repo)
    {
        _repo = repo;
    }

    public List<Reserva> Ejecutar()
    {
        return _repo.ObtenerTodas()
            .Where(r => r.EstadoAsistencia == Estado.Pendiente || r.EstadoAsistencia == Estado.Asistio)
            .ToList();
    }
}
