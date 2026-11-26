string archivo = "movimientos.csv";

if (!File.Exists(archivo))
{
    Console.WriteLine("No se encontró el archivo movimientos.csv");
    return;
}

using StreamReader reader = new StreamReader(archivo);

string? linea;

// Omitir cabecera
reader.ReadLine();

Console.WriteLine();
Console.WriteLine("Contenido del archivo:");
Console.WriteLine("----------------------");

while ((linea = reader.ReadLine()) != null) 
{ 
    string[] datos = linea.Split(';'); 
    Console.WriteLine(); Console.WriteLine("Nueva fila"); 
    Console.WriteLine($"DNI: {datos[0]}"); 
    Console.WriteLine($"Fecha: {datos[1]}"); 
    Console.WriteLine($"Tipo: {datos[2]}"); 
    Console.WriteLine($"Importe: {datos[3]}"); 
}