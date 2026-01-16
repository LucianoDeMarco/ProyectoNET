using centroDeportivo.Aplicacion.interfaces;

namespace centroDeportivo.Aplicacion.CasosDeUso.Reservas;

public class ListarReservasUseCase
{
    private readonly IReservaRepository _repo;

    public ListarReservasUseCase (IReservaRepository repo)
    {
        _repo = repo;
    }

    public List<Reserva> Ejecutar()
    {
        return _repo.ObtenerTodas();
    }

}