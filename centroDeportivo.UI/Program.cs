using centroDeportivo.Aplicacion.Interfaces;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Repositorios;
using centroDeportivo.UI.Components;
using centroDeportivo.Aplicacion.CasosDeUso;
using centroDeportivo.Aplicacion.Validadores;
using centroDeportivo.Aplicacion.CasosDeUso.Actividades;
using centroDeportivo.Aplicacion.interfaces;
using centroDeportivo.UI.Servicios;
using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.CasosDeUso.Reservas;
using centroDeportivo.Aplicacion.CasosDeUso.Personas;

var builder = WebApplication.CreateBuilder(args);

//Servicio Hash
builder.Services.AddSingleton<ServicioHash>();

builder.Services.AddDbContext<CentroDeportivoContext>();
// --- REPOSITORIOS ---
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IActividadRepository, ActividadRepositorioDB>(); 
builder.Services.AddScoped<IPersonaRepository, PersonaRepositorioDB>();
builder.Services.AddScoped<IReservaRepository, ReservaRepositorioDB>();

builder.Services.AddScoped<SesionService>();

builder.Services.AddScoped<IServicioAutorizacion, ServicioAutorizacion>(); 
builder.Services.AddTransient<ValidacionesUsuario>();

// --- CASOS DE USO: USUARIOS ---
builder.Services.AddTransient<LoginUseCase>();
builder.Services.AddTransient<ListarUsuariosUseCase>();
builder.Services.AddTransient<AgregarUsuarioUseCase>();
builder.Services.AddTransient<ModificarUsuarioUseCase>();
builder.Services.AddTransient<EliminarUsuarioUseCase>();  

// --- CASOS DE USO: Actividad
builder.Services.AddTransient<AltaActividadUseCase>();
builder.Services.AddTransient<ModificarActividadUseCase>();
builder.Services.AddTransient<BajaActividadUseCase>();
builder.Services.AddTransient<ListarActividadesUseCase>();
builder.Services.AddTransient<ListarPersonasUseCase>();

//--- CASOS DE USO: Reserva
builder.Services.AddTransient<CancelarReservaUseCase>();
builder.Services.AddTransient<ListarReservasUseCase>();
builder.Services.AddTransient<ReservarActividadUseCase>();


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CentroDeportivoContext>();
    context.Database.EnsureCreated();

    if (!context.Personas.Any())
    {
        // Creo docentes que dicten la clase asi no programo esta parte
        context.Personas.Add(new Docente 
        { 
            Nombre = "Lionel", 
            Apellido = "Messi", 
            Matricula = "M-1010",           
            AnioIngreso = DateTime.Now.AddYears(-2), 
            
            NroCarnet = 1001,
            Mail = "leo@seleccion.com",
            Direccion = "Miami",
            Telefono = "555-1010",
            Facultad = "Fútbol"
        });
        
        context.Personas.Add(new Docente 
        { 
            Nombre = "Emiliano", 
            Apellido = "Martínez", 
            Matricula = "M-2323", 
            AnioIngreso = DateTime.Now.AddYears(-1),
            
            NroCarnet = 1002,
            Mail = "dibu@seleccion.com",
            Direccion = "Birmingham",
            Telefono = "555-2323",
            Facultad = "Arqueros"
        });

        context.SaveChanges(); 
        Console.WriteLine("¡Profesores de prueba creados exitosamente!");
    }
}

app.Run();
