using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Aplicacion.Validadores;

public class AgregarUsuarioUseCase
{
    private readonly IUsuarioRepositorio _repo;
    private readonly ValidacionesUsuario _validador;

    public AgregarUsuarioUseCase(IUsuarioRepositorio repo, ValidacionesUsuario validador)
    {
        _repo = repo;
        _validador = validador;
    }

    public void Ejecutar(Usuario nuevoUsuario)
    {
        // 1. Validar datos básicos
        _validador.Validar(nuevoUsuario);

        // 2. Lógica de primer usuario (Admin)
        var todos = _repo.ListarUsuarios();
        if (todos.Count == 0)
        {
            nuevoUsuario.ListaPermisos = Enum.GetValues<Permiso>().ToList();
        }

        // 3. Guardar
        _repo.AgregarUsuario(nuevoUsuario);
    }
}

internal class UsuarioValidador
{
}