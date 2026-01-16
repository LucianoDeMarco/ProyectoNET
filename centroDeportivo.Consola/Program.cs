using centroDeportivo.Aplicacion;
using centroDeportivo.Aplicacion.CasosDeUso.Personas;
using centroDeportivo.Aplicacion.CasosDeUso.Actividades;
using centroDeportivo.Aplicacion.CasosDeUso.Reservas;
using centroDeportivo.Aplicacion.Seguridad;
using centroDeportivo.Repositorios;

int idAdmin = 1; // usuario autorizado

// Repositorios
var actividadRepo = new ActividadRepositorioArchivo();
var reservaRepo = new ReservaRepositorioArchivo();

// Autorización
var autorizacion = new ServicioAutorizacionProvisorio();

// ================== PERSONAS ==================
var altaPersona = new AltaPersonaUseCase(personaRepo, autorizacion);

var docente = new Docente
{
    Id = 1,
    NroCarnet = 123,
    Nombre = "Ana",
    Apellido = "Gómez",
    Mail = "ana@gmail.com",
    Matricula = "DOC-001",
    AnioIngreso = DateTime.Now
};

altaPersona.Ejecutar(idAdmin, docente);

// ================== ACTIVIDADES ==================
var altaActividad = new AltaActividadUseCase(actividadRepo, autorizacion);

var actividad = new ActividadDeportiva
{
    Id = 1,
    Nombre = "Yoga",
    DiasDisponibles = "Lunes y Miércoles",
    CupoMaximo = 2,
    Responsable = docente
};

altaActividad.Ejecutar(idAdmin, actividad);

// ================== RESERVAR ACTIVIDAD ==================
var reservarActividad = new ReservarActividadUseCase(
    reservaRepo,
    actividadRepo,
    autorizacion);

var reserva = new Reserva
{
    Id = 1,
    PersonaId = docente.Id,
    ActividadId = actividad.Id,
    FechaReserva = DateTime.Today
};

reservarActividad.Ejecutar(idAdmin, reserva);

// ================== LISTAR RESERVAS ACTIVAS ==================
var listarActivas = new ListarReservasActivasUseCase(reservaRepo);

var reservasActivas = listarActivas.Ejecutar();

Console.WriteLine("Reservas activas:");
foreach (var r in reservasActivas)
{
    Console.WriteLine($"Reserva {r.Id} - Actividad {r.ActividadId}");
}
