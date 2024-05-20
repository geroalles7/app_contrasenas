using Microsoft.Win32;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using BCrypt.Net;




namespace app_contraseñas
{
    internal class Administrador
    {
        private string cadenaConexion = "Host=localhost;Username=postgres;Password=Gero2002;Database=contraseñas";
        List<Contrasena> contrasenas = new List<Contrasena>();

        
        public List<Contrasena> GetMiLista()
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "SELECT * FROM contraseñas ORDER BY id";
                using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                {
                    using (NpgsqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contrasena contrasena = new Contrasena
                            {
                                Id = Convert.ToInt32(reader["id"]),   
                                Aplicacion = reader["app"].ToString(),
                                Nombre_usuario = reader["usuario"].ToString(),
                                Contraseña = reader["contraseña"].ToString(),
                                Fecha = Convert.ToDateTime(reader["fechacreacion"])
                            };
                            contrasenas.Add(contrasena);
                        }
                    }
                }
            }

            return contrasenas;
        }
        
        public int getTamañoLista()
        {
            return contrasenas.Count();
        }
        public string EncriptarContraseña(string contraseña)
        {
            return BCrypt.Net.BCrypt.HashPassword(contraseña);
        }
        public void CrearContrasena(string app, string usuario, string contraseña, DateTime fechaCreacion)
        {

            //string contraseñaEncriptada = EncriptarContraseña(contraseña);
            using (NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "INSERT INTO contraseñas (app, usuario, contraseña, fechacreacion) VALUES (@app, @usuario, @contraseña, @fechaCreacion)";
                using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@app", app);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@contraseña", contraseña);
                    comando.Parameters.AddWithValue("@fechacreacion", fechaCreacion);

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Contraseña creada con exito");
                }
            }
            
        }

        public void ActualizarContrasena(int id, string app, string usuario, string contraseña)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "UPDATE contraseñas SET app = @app, usuario = @usuario, contraseña = @contraseña WHERE id = @id";
                using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@app", app);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@contraseña", contraseña);


                    comando.ExecuteNonQuery();
                    MessageBox.Show("Contraseña actualizada con exito");
                }
            }
        }

        public void EliminarContrasena(int id)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "DELETE FROM contraseñas WHERE id = @id";
                using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Contraseña eliminada con exito");
                }
            }
        }

        public DataTable GetPasswords()
        {
            DataTable dataTable = new DataTable();

            using (NpgsqlConnection connection = new NpgsqlConnection(cadenaConexion))
            {
                try
                {
                    connection.Open();

                    string sql = "SELECT id,app, usuario, contraseña AS contraseña, fechacreacion AS modificado FROM contraseñas";

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, connection))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
                }
            }

            return dataTable;
        }


    }
    


    
}
