using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion.interfaces;

namespace centroDeportivo.Aplicacion.CasosDeUso.Reservas;

public class BajaReservaUseCase
{
    private readonly IReservaRepository _repo;

    public BajaReservaUseCase(IReservaRepository repo)
    {
        _repo = repo;
    }

    public void Ejecutar(int id)
    {
        var reserva = _repo.ObtenerPorId(id);

        if (reserva == null)
        {
            throw new ValidacionException("No existe una reserva con el Id indicado.");
        }

        _repo.Eliminar(id);
    }
}
