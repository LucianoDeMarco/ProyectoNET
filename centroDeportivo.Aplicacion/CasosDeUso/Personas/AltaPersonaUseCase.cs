using centroDeportivo.Aplicacion.Excepciones;
using centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.Aplicacion.Validaciones;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion;

namespace centroDeportivo.Aplicacion.CasosDeUso.Personas;

public class AltaPersonaUseCase
{
    private readonly IPersonaRepository _repo;
    private readonly IServicioAutorizacion _autorizacion;

    public AltaPersonaUseCase(
        IPersonaRepository repo,
        IServicioAutorizacion autorizacion)
    {
        _repo = repo;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(Usuario idUsuario, Persona persona)
    {
        if (!_autorizacion.PoseePermiso(idUsuario, Permiso.UsuarioAlta))
        {
            throw new ValidacionException("El usuario no tiene permiso para dar de alta personas.");
        }

        if (!ValidacionesResponsable.EsValido(persona, _repo))
        {
            throw new ValidacionException("Datos de la persona inválidos.");
        }

        _repo.Guardar(persona);
    }
}
