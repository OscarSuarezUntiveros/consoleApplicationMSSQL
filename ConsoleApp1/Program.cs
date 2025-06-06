using System;
using System.Data.SqlClient;
using ConsoleApp1;
using Microsoft.Win32;

class Program
{
    static void Main()
    {
        var connectionString = "Data Source=.;Initial Catalog=BPCS_ODS;Integrated Security=True";
        var repo = new VentaRepository(connectionString);
        
        Console.Write("¿Cuántos registros desea insertar?");
        int cantidad = int.Parse(Console.ReadLine());

        for (int i = 0; i < cantidad; i++)
        {
            Console.WriteLine($"\n🔹 Registro #{i + 1}");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Duración (ej. 2.5): ");
            double duracion = double.Parse(Console.ReadLine());
            var nuevaVenta = new Venta
            {
                Nombre =nombre,
                Fecha = DateTime.Now,
                Duracion = duracion
            };
            try
            {
                repo.InsertarVenta(nuevaVenta);
                Console.WriteLine("✅ Registro insertado con éxito.");
                var ventas = repo.ObtenerVentas();
                Console.WriteLine("📋 Lista de ventas:");

                foreach (var venta in ventas)
                { 
                    Console.WriteLine($"ID: {venta.Id}, Cliente: {venta.Nombre}, Fecha: {venta.Fecha}, Total: {venta.Duracion}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error al insertar: " + ex.Message);
            }
        }        
    }
}
