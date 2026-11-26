# ImportadorMovimientos

Trabajo final curso .NET - Importación de movimientos CSV.

## Tecnologías

- .NET 10
- Entity Framework Core
- SQL Server LocalDB
- SQL Server Management Studio
- Git y GitHub

## Funcionalidad implementada

- Creación de base de datos SQL Server.
- Generación de entidades mediante Scaffold-DbContext.
- Configuración mediante appsettings.json.
- Conexión a SQL Server mediante EF Core.
- Lectura secuencial de movimientos.csv.
- Omisión de cabecera.
- Parseo básico de filas utilizando Split(';').

## Estado del proyecto

✅ Requisito 1 completado

Verificar que el archivo exista y recorrer sus filas sin cargarlo completo en memoria.

## Próximo desarrollo

Implementar las validaciones requeridas:

- Cantidad de columnas.
- Cliente existente.
- Tipo de movimiento existente.
- Fecha válida.
- Importe numérico.
- Importe mayor que cero.