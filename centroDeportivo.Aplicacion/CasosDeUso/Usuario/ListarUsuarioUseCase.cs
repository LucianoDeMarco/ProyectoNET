using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion;
public class ListarUsuariosUseCase
{
    private readonly IUsuarioRepositorio _repo;

    public ListarUsuariosUseCase(IUsuarioRepositorio repo) => _repo = repo;

    public List<Usuario> Ejecutar() => _repo.ListarUsuarios();
}