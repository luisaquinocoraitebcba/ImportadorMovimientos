using ImportadorMovimientos.Data;
using ImportadorMovimientos.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ImportadorMovimientos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString =
                config.GetConnectionString("DefaultConnection");

            var opciones = new DbContextOptionsBuilder<BancoDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var contexto = new BancoDbContext(opciones);

            string archivo = "movimientos.csv";

            if (!File.Exists(archivo))
            {
                Console.WriteLine(
                    "No se encontró el archivo movimientos.csv"
                );

                return;
            }

            using StreamReader reader = new StreamReader(archivo);

            string? linea;

            // Omitir cabecera
            reader.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Contenido del archivo:");
            Console.WriteLine("----------------------");

            int numeroLinea = 1;

            int filasValidas = 0;
            int filasRechazadas = 0;

            while ((linea = reader.ReadLine()) != null)
            {
                numeroLinea++;

                string[] datos = linea.Split(';');

                // Validar cantidad de columnas
                if (datos.Length != 4)
                {
                    filasRechazadas++;

                    Console.WriteLine(
                        $"Línea {numeroLinea}: cantidad de columnas incorrecta."
                    );

                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("Nueva fila");

                Console.WriteLine($"DNI: {datos[0]}");
                Console.WriteLine($"Fecha: {datos[1]}");
                Console.WriteLine($"Tipo: {datos[2]}");
                Console.WriteLine($"Importe: {datos[3]}");

                // Validar cliente existente
                var cliente = contexto.Clientes
                    .FirstOrDefault(c => c.Dni == datos[0]);

                if (cliente == null)
                {
                    filasRechazadas++;

                    Console.WriteLine(
                        $"Línea {numeroLinea}: cliente inexistente."
                    );

                    continue;
                }

                

                Console.WriteLine(
                    $"Línea {numeroLinea}: cliente válido."
                );
                //valido movimiento
                var tipoMovimiento = contexto.TiposMovimientos
                        .FirstOrDefault(t => t.Codigo == datos[2]);

                if (tipoMovimiento == null)
                {
                    filasRechazadas++;

                    Console.WriteLine(
                        $"Línea {numeroLinea}: tipo de movimiento inexistente."
                    );

                    continue;
                }
                Console.WriteLine($"Línea {numeroLinea}: tipo válido.");

                //valido fecha
                if (!DateOnly.TryParse(datos[1], out DateOnly fecha))
                {
                    filasRechazadas++;

                    Console.WriteLine(
                        $"Línea {numeroLinea}: fecha inválida."
                    );

                    continue;
                }

                Console.WriteLine(
                    $"Línea {numeroLinea}: fecha válida."
                );

                //valido importe
                if (!decimal.TryParse(datos[3], out decimal importe))
                {
                    filasRechazadas++;

                    Console.WriteLine(
                        $"Línea {numeroLinea}: importe inválido."
                    );

                    continue;
                }

                //valido importe < 0
                if (importe <= 0)
                {
                    filasRechazadas++;

                    Console.WriteLine(
                        $"Línea {numeroLinea}: importe debe ser mayor que cero."
                    );

                    continue;
                }

                filasValidas++;
                Console.WriteLine($"Línea {numeroLinea}: fila válida.");
                
                //para insertar movimiento
                var movimiento = new Movimiento
                {
                    ClienteId = cliente.Id,
                    TipoMovimientoId = tipoMovimiento.Id,
                    Fecha = fecha,
                    Importe = importe
                };

                contexto.Movimientos.Add(movimiento);

                contexto.SaveChanges();

                filasValidas++;

                Console.WriteLine(
                    $"Línea {numeroLinea}: movimiento guardado."
                );



            }

            Console.WriteLine();
            Console.WriteLine("----------------------");
            Console.WriteLine($"Filas válidas: {filasValidas}");
            Console.WriteLine($"Filas rechazadas: {filasRechazadas}");

            Console.ReadKey();
        }
    }
}
