using centroDeportivo.Aplicacion;
namespace centroDeportivo.UI.Servicios
{
    public class SesionService
    {
        // Evento para avisar a los componentes (como el NavMenu) que el usuario cambió
        public event Action? OnChange;

        public Usuario? UsuarioActual { get; private set; }

        // Propiedad rápida para saber si hay alguien logueado
        public bool EstaLogueado => UsuarioActual != null;

        public void IniciarSesion(Usuario usuario)
        {
            UsuarioActual = usuario;
            NotifyStateChanged();
        }

        public void CerrarSesion()
        {
            UsuarioActual = null;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}