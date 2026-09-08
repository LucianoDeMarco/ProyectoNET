# Sistema de Gestión del Centro Deportivo Universitario

Proyecto individual desarrollado para la materia **Seminario de Lenguajes (opción .NET)** — UNLP, 1er semestre 2025.

## 📋 Descripción

Sistema para gestionar las actividades y el uso de instalaciones deportivas de un centro universitario: registro de personas (estudiantes y docentes), actividades deportivas, reservas de instalaciones, control de asistencia y gestión de usuarios con permisos.

El proyecto se desarrolló en dos entregas: una primera versión funcional por consola con persistencia en archivos de texto plano, y una segunda entrega (la actual) que la expande con una interfaz web en Blazor, persistencia real en base de datos y un sistema de autenticación y autorización completo.

## ⚙️ Funcionalidades

- ABM (altas, bajas, modificaciones) y listados de personas, actividades y reservas.
- Reserva de actividades con validación de cupo disponible.
- Cancelación de reservas y control de estado de asistencia (pendiente, asistió, ausente, cancelada).
- Registro e inicio de sesión de usuarios, con contraseñas hasheadas — nunca se almacenan en texto plano.
- Sistema de permisos granular: el primer usuario registrado es Administrador con todos los permisos; el resto arranca con permisos de solo lectura y puede recibir permisos adicionales.
- Servicio de autorización (`IServicioAutorizacion`) que valida en cada operación si el usuario tiene el permiso necesario — reemplaza al servicio provisorio de la primera entrega.

## 🏗️ Arquitectura

Solución dividida en capas, para separar responsabilidades y facilitar el testing:

- **centroDeportivo.Aplicacion** — entidades del dominio, validadores, casos de uso (`ReservarActividadUseCase`, `CancelarReservaUseCase`, `LoginUseCase`, etc.) e interfaces de repositorio. No depende de ninguna tecnología de persistencia ni de UI.
- **centroDeportivo.Repositorios** — implementación de los repositorios con Entity Framework Core (code-first) sobre SQLite.
- **centroDeportivo.UI** — interfaz de usuario en Blazor Server (componentes interactivos del lado del servidor).

Todo se conecta por inyección de dependencias configurada en `Program.cs`, y se accede siempre a través de interfaces — la persistencia pasó de archivos de texto a una base SQLite real sin tener que tocar la lógica de negocio.

## 🔐 Seguridad

- Las contraseñas se hashean (`ServicioHash`) antes de guardarse; nunca se persiste la contraseña en texto plano.
- Los permisos se verifican en cada operación sensible mediante el servicio de autorización.

## 🛠️ Tecnologías

C# · .NET 8 · Entity Framework Core (Migrations, code-first) · SQLite · Blazor Server · Inyección de dependencias

## 🚀 Cómo ejecutar

1. Cloná el repositorio y abrí `centroDeportivo.sln` en Visual Studio (o VS Code con la extensión de C#).
2. Asegurate de tener el **.NET 8 SDK** instalado.
3. Establecé `centroDeportivo.UI` como proyecto de inicio.
4. Ejecutá con F5 (o `dotnet run` desde la carpeta `centroDeportivo.UI`). Se abre automáticamente el navegador.
5. La base de datos SQLite (`centroDeportivo.db`) se crea sola en el primer arranque si no existe, con un par de docentes de ejemplo precargados.
6. Registrate como usuario desde la pantalla de bienvenida — el primer usuario que se registra queda como Administrador con todos los permisos.

## 📚 Qué aprendí

Lo que más laburo me dio fue el salto de la primera entrega a esta: pasar de un servicio de autorización hardcodeado (un único ID de administrador) a uno que realmente valida permisos por usuario contra la base de datos. Ahí tuve que pensar bien cómo modelar una lista de permisos dentro de una única columna de SQLite usando `HasConversion` en EF Core. También fue la primera vez que implementé hash de contraseñas en un proyecto propio, entendiendo por qué nunca hay que guardar la contraseña en texto plano y cómo se verifica sin poder "desarmar" el hash.

## 👤 Autor

Luciano De Marco — [GitHub](https://github.com/LucianoDeMarco) · [LinkedIn](https://www.linkedin.com/in/luciano-de-marco-88b99836a)
