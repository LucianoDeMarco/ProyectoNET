using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.Interfaces;

public class ListarUsuariosUseCase
{
    private readonly IUsuarioRepositorio _repo;

    public ListarUsuariosUseCase(IUsuarioRepositorio repo) => _repo = repo;

    public List<Usuario> Ejecutar() => _repo.ListarUsuarios();
}