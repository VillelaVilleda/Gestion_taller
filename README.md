# GestorTaller

Sistema de gestión de talleres mecánicos y centros de servicios técnicos. Aplicación de escritorio (.NET 10, WinForms) con base de datos PostgreSQL. Proyecto del curso Análisis y Diseño de Datos, desarrollado con TDD (xUnit) y organizado en sprints.

Controla el ciclo completo de una orden de trabajo: Recepción, Diagnóstico, Cotización, Reparación, Terminado y Entregado. También gestiona clientes, repuestos (existencia y precio) y empleados con rol de administrador o usuario común.

## Requisitos

- .NET SDK 10
- Docker Desktop
- Visual Studio 2022 o superior (opcional, también funciona con la línea de comandos)

## Puesta en marcha

Los comandos son de PowerShell. Las rutas parten de la raíz del repositorio.

### 1. Base de datos (Docker)

Crear `docker/.env` con tus propios valores (el archivo no se sube a Git):

```
POSTGRES_USER=<usuario>
POSTGRES_PASSWORD=<clave>
POSTGRES_DB=<base>
```

Levantar el contenedor:

```powershell
cd docker
docker compose up -d
```

### 2. Cadena de conexión

La aplicación lee la conexión de la variable de entorno `GESTORTALLER_CONNECTION_STRING`; no se guarda en el código. Definirla con los mismos datos de `docker/.env`:

```powershell
setx GESTORTALLER_CONNECTION_STRING "Host=localhost;Port=5432;Database=<base>;Username=<usuario>;Password=<clave>"
```

Cerrar y volver a abrir la terminal (o Visual Studio) para que la variable se aplique. Si falta, la aplicación avisa qué hay que definir.

### 3. Migraciones

Desde `src\GestorTaller`:

```powershell
dotnet ef database update --project GestorTaller.Datos --startup-project GestorTaller
```

Las migraciones están en `src/GestorTaller/GestorTaller.Datos/Migrations`. La aplicación no migra la base al iniciar.

### 4. Datos de prueba (opcional)

Solo sobre una base recién creada y migrada:

```powershell
Get-Content docker\seed_datos_prueba.sql | docker exec -i gestor_taller_db psql -U <usuario> -d <base>
```

Carga clientes, repuestos, órdenes en distintos estados y estos empleados:

| Usuario | Nombre | Rol |
|---|---|---|
| `admin` | Administrador | Administrador |
| `mgonzalez` | Maria Gonzalez | Administrador |
| `usuario` | Empleado de prueba | Usuario común |
| `cmendez` | Carlos Mendez | Usuario común |

Las contraseñas de prueba están en los comentarios de `docker/seed_datos_prueba.sql`. Son solo para desarrollo local.

### 5. Ejecutar

Desde `src\GestorTaller`:

```powershell
dotnet run --project GestorTaller
```

Si la base no responde o ocurre un error inesperado, la aplicación muestra un mensaje claro y lo registra en `%LocalAppData%\GestorTaller\logs\errores-AAAA-MM-DD.log`.

## Pruebas

Desde `src\GestorTaller`:

```powershell
dotnet test
```

## Roles

- **Usuario común:** crea órdenes, clientes y repuestos, y avanza las órdenes por sus etapas.
- **Administrador:** además gestiona empleados (crear, editar nombre, usuario y rol, restablecer contraseña y eliminar), edita y elimina repuestos y clientes, y edita y elimina órdenes no entregadas. Las órdenes entregadas no se editan ni se eliminan.

Los empleados nuevos reciben un usuario y una contraseña temporal generados por el sistema, que se muestra una sola vez. Las contraseñas se guardan con hash (BCrypt).

## Estructura

```
src/GestorTaller/
  GestorTaller/            Interfaz (WinForms)
  GestorTaller.Negocio/    Reglas de negocio, modelos y contratos de repositorio
  GestorTaller.Datos/      Entity Framework Core + PostgreSQL, migraciones
  GestorTaller.Tests/      Pruebas unitarias (xUnit)
docker/                    docker-compose y datos de prueba
docs/                      Guía de Entity Framework Core, DERCAS, DER y DFD
```

La capa de negocio no depende de las demás: se comunica con los datos mediante interfaces (patrón Repository).

## Documentación

- `docs/Guia_Entity_Framework_Core.md`: uso de EF Core y migraciones.
- `docs/DERCAS_V3.pdf`: especificación de requisitos y criterios de aceptación.

## Créditos

Los íconos de la interfaz son de [Lucide](https://lucide.dev/) (licencia ISC; algunos derivan de Feather, licencia MIT). El aviso de licencia está en `src/GestorTaller/GestorTaller/Recursos/Iconos/LICENSE-Lucide.txt`.
