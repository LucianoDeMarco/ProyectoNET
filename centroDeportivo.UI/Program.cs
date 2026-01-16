using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Repositorios;
using centroDeportivo.UI.Components;
using centroDeportivo.Aplicacion.CasosDeUso;
using centroDeportivo.Aplicacion.Validadores;
using centroDeportivo.Aplicacion.CasosDeUso.Actividades;
using centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.UI.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Registrar el DbContext de Entity Framework
builder.Services.AddDbContext<CentroDeportivoContext>();
// --- REPOSITORIOS ---
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IActividadRepository, ActividadRepositorioDB>(); // Asegúrate que el nombre sea exacto
builder.Services.AddScoped<IPersonaRepository, PersonaRepositorioDB>();
builder.Services.AddScoped<IReservaRepository, ReservaRepositorioDB>();

builder.Services.AddScoped<SesionService>();
// Agrega aquí tus otros repositorios (IActividadRepositorio, etc.)

// --- SEGURIDAD Y VALIDACIÓN ---
// CAMBIO: Usa el servicio real, no el provisorio
builder.Services.AddScoped<IServicioAutorizacion, ServicioAutorizacion>(); 
builder.Services.AddTransient<ValidacionesUsuario>();

// --- CASOS DE USO: USUARIOS ---
builder.Services.AddTransient<LoginUseCase>();
builder.Services.AddTransient<ListarUsuariosUseCase>();
builder.Services.AddTransient<AgregarUsuarioUseCase>();
builder.Services.AddTransient<ModificarUsuarioUseCase>(); // Necesario para gestionar permisos
builder.Services.AddTransient<EliminarUsuarioUseCase>();  // Necesario para dar de baja

// --- CASOS DE USO: NEGOCIO (Los de la primera entrega) ---
// No los olvides, Blazor los necesita para las otras páginas
builder.Services.AddTransient<AltaActividadUseCase>();
builder.Services.AddTransient<ListarActividadesUseCase>();
// ... registrar el resto de tus casos de uso existentes
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
