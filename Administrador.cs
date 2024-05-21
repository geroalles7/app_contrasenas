﻿using Microsoft.Win32;
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
using System.IO;




namespace app_contraseñas
{
    internal class Administrador
    {
        private string cadenaConexion = "Host=localhost;Username=postgres;Password=Gero2002;Database=contraseñas";
        List<Contrasena> contrasenas = new List<Contrasena>();
        List<Usuario>usuarios=new List<Usuario>();

        
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
                                Fecha = Convert.ToDateTime(reader["modificado"])
                            };
                            contrasenas.Add(contrasena);
                        }
                    }
                }
            }

            return contrasenas;
        }

        public List<Usuario> GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "SELECT * FROM usuarios ORDER BY id";
                using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                {
                    using (NpgsqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario u = new Usuario
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nombre = reader["nombre"].ToString(),
                                Contraseña = reader["contraseña"].ToString()
                            };
                            usuarios.Add(u);
                        }
                    }
                }
            }

            return usuarios;
        }


        public void AgregarUsuario(string nombre, string contraseña)
        {
            
            
            using (NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "INSERT INTO usuarios (nombre, contraseña) VALUES (@nombre, @contraseña)";
                using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                {
                    
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    comando.Parameters.AddWithValue("@contraseña", contraseña);
                    

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Usuario creado con exito");
                }
            }
        }
        public int getTamañoLista()
        {
            return contrasenas.Count();
        }
        public string EncriptarContraseña(string contraseña)
        {
            return BCrypt.Net.BCrypt.HashPassword(contraseña);
        }
        public void CrearContrasena(string app, string usuario, string contraseña, DateTime modificado, int usuario_id)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "INSERT INTO contraseñas (app, usuario, contraseña, modificado, usuario_id) VALUES (@app, @usuario, @contraseña, @modificado, @usuario_id)";
                using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@app", app);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@contraseña", contraseña);
                    comando.Parameters.AddWithValue("@modificado", modificado);
                    comando.Parameters.AddWithValue("@usuario_id", usuario_id);

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Contraseña creada con éxito");
                }
            }
        }


        public void ActualizarContrasena(int id, string app, string usuario, string contraseña, DateTime modificado)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "UPDATE contraseñas SET app = @app, usuario = @usuario, contraseña = @contraseña, modificado = @modificado WHERE id = @id";
                using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@app", app);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@contraseña", contraseña);
                    comando.Parameters.AddWithValue("@modificado", modificado);


                    comando.ExecuteNonQuery();
                    MessageBox.Show("Contraseña actualizada con exito");
                }
            }
        }

        public void ActualizarUsuario(int id,string nombre, string contraseña)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "UPDATE usuarios SET nombre = @nombre,contraseña = @contraseña WHERE id = @id";
                using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    comando.Parameters.AddWithValue("@contraseña", contraseña);
                    


                    comando.ExecuteNonQuery();
                    MessageBox.Show("Usuario actualizado con exito");
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

        public DataTable GetPasswords(int usuario_id)
        {
            DataTable dataTable = new DataTable();

            using (NpgsqlConnection connection = new NpgsqlConnection(cadenaConexion))
            {
                try
                {
                    connection.Open();

                    string sql = "SELECT id, app, usuario, contraseña AS contraseña, modificado AS modificado FROM contraseñas WHERE usuario_id = @usuario_id";
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@usuario_id", usuario_id);
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
