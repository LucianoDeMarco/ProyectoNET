using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.Aplicacion.Validaciones;
using centroDeportivo.Aplicacion;

namespace centroDeportivo.Aplicacion.CasosDeUso.Personas;

public class ModificarPersonaUseCase
{
    private readonly IPersonaRepository _repo;

    public ModificarPersonaUseCase(IPersonaRepository repo)
    {
        _repo = repo;
    }

    public void Ejecutar(Persona persona)
    {
        var per = _repo.ObtenerPorId(persona.Id);

        if (per == null)
        {
            throw new ValidacionException("No existe una persona con el Id indicado.");
        }

        if (!ValidacionesResponsable.EsValido(persona, _repo))
        {
            throw new ValidacionException("Datos inválidos para la persona.");
        }


        _repo.Modificar(persona);
    }
}
