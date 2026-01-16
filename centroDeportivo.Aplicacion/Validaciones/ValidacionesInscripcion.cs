using centroDeportivo.Aplicacion.interfaces;
namespace centroDeportivo.Aplicacion.Validaciones;

public static class ValidacionesInscripcion
{
    public static bool EsValida(Reserva nuevaReserva, IReservaRepository repo)
    {

        bool fechaOk = nuevaReserva.FechaReserva.Date >= DateTime.Today;

        if (!fechaOk) return false;

        bool yaEstaInscrito = repo.ExisteInscripcion(nuevaReserva.PersonaId, nuevaReserva.ActividadId);

        return !yaEstaInscrito; 
    }
}