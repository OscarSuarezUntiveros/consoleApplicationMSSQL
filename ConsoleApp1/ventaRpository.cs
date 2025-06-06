using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    public class VentaRepository
    {
        private string connectionString;


        public VentaRepository(string conn)
        {
            connectionString = conn;
        }

        public void InsertarVenta(Venta venta)
        {
            try {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO dbo.Ventas (Nombre, Fecha, Duracion) VALUES (@Nombre, @Fecha, @Duracion)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nombre", venta.Nombre);
                    cmd.Parameters.AddWithValue("@Fecha", venta.Fecha);
                    cmd.Parameters.AddWithValue("@Duracion", venta.Duracion);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Ocurrió un error: " + ex.Message);
            }
        }
        //DESKTOP-0ISKABP
        public List<Venta> ObtenerVentas()
        {
        
            var ventas = new List<Venta>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, nombre, Fecha, duracion FROM dbo.Ventas";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ventas.Add(new Venta
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Fecha = reader.GetDateTime(2),
                        Duracion = reader.GetDouble(3)
                    });
                }
            }

            return ventas;
        }
    }
}
