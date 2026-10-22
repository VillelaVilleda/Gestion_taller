# Uso de Entity Framework Core en GestorTaller

Resumen técnico para el supervisor: cómo se usa Entity Framework Core (EF Core) para guardar los datos y cómo se manejan los cambios de la base de datos.

## 1. Resumen

- **ORM:** Entity Framework Core 10 con el proveedor Npgsql.
- **Base de datos:** PostgreSQL 16, en un contenedor Docker (`gestor_taller_db`).
- **Enfoque:** *Code First*. Las clases del dominio definen las tablas y los cambios se versionan con migraciones.
- **Proyecto:** `GestorTaller.Datos`.

## 2. Dónde se usa

La capa de negocio (`GestorTaller.Negocio`) **no conoce EF Core**. Solo define las interfaces (`IOrdenRepository`, `IClienteRepository`, `IEmpleadoRepository`, `IRepuestoRepository`), y `GestorTaller.Datos` las implementa (`OrdenRepositoryEfCore`, etc.). Así las reglas de negocio se prueban sin base de datos.

## 3. Componentes principales

- **`GestorTallerDbContext`:** tiene un `DbSet` por tabla (`Clientes`, `Empleados`, `Ordenes`, `Repuestos`, `OrdenRepuestos`). El modelo se configura en `OnModelCreating` con *Fluent API*, sin atributos en las entidades.
- **`ConexionBaseDeDatos`:** devuelve la cadena de conexión. Lee la variable de entorno `GESTORTALLER_CONNECTION_STRING`; si no está definida, la aplicación indica qué falta. La cadena no se guarda en el código.
- **`GestorTallerDbContextFactory`:** le indica a `dotnet ef` cómo crear el contexto al generar migraciones. La aplicación no lo usa al ejecutarse.
- **`Program.cs`:** crea el `DbContext`, lo comparte entre los repositorios y lo entrega a los formularios.
- **Repositorios (`*RepositoryEfCore`):** reciben el `DbContext` y encapsulan las consultas. `OrdenRepositoryEfCore` carga las relaciones con `Include` y guarda cada operación con un solo `SaveChanges()`, de modo que se aplica completa o no se aplica.

## 4. Decisiones de diseño

- Tablas y columnas en `snake_case`.
- Importes con `numeric(10,2)` y restricciones `CHECK` (`precio >= 0`, `cantidad > 0`).
- El estado de la orden se guarda como texto, y su historial en la tabla `orden_historial_estado`.
- `orden_repuesto` resuelve la relación entre órdenes y repuestos, con cantidad y precio unitario al cotizar.
- Los empleados responsables de cada etapa de la orden son llaves foráneas hacia `empleado`.
- Las fechas se guardan en UTC y se convierten a hora local al leerlas, porque PostgreSQL solo acepta UTC.

## 5. Migraciones

Se ejecutan desde `src\GestorTaller`:

```powershell
# Crear una migración
dotnet ef migrations add NombreDeLaMigracion --project GestorTaller.Datos --startup-project GestorTaller

# Aplicar las migraciones pendientes
dotnet ef database update --project GestorTaller.Datos --startup-project GestorTaller
```

La aplicación no migra la base al iniciar; se hace con el segundo comando. Las migraciones están en `GestorTaller.Datos/Migrations` y se suben a Git, así cualquier integrante obtiene el mismo esquema. Migraciones actuales: `InicialSprint4`, `RenombrarUsuarioAEmpleado`, `AgregarRepuesto`, `AgregarOrdenRepuesto`, `AgregarPrecioRepuestos` y `RelacionarEmpleadosEnOrden`.

## 6. Cómo reproducir el entorno

```powershell
cd docker
docker compose up -d
cd ..\src\GestorTaller
dotnet ef database update --project GestorTaller.Datos --startup-project GestorTaller
```

Las credenciales salen de `docker/.env`, que no se sube a Git. Antes de ejecutar la aplicación o `dotnet ef`, hay que definir `GESTORTALLER_CONNECTION_STRING` con los mismos datos, por ejemplo `Host=localhost;Port=5432;Database=<base>;Username=<usuario>;Password=<clave>`. Para cargar datos de prueba, ejecutar `docker/seed_datos_prueba.sql` solo en una base recién creada.
