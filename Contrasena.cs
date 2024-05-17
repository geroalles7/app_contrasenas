using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_contraseñas
{
    internal class Contrasena
    {
        string aplicacion;
        string nombre_usuario;
        string contrasena;
        DateTime fecha;
        int id;
        


        public Contrasena(string aplicacion, string nombre_usuario, string contrasena, DateTime fecha  )
        {
            id++;
            this.aplicacion = aplicacion;
            this.contrasena = contrasena;
            this.nombre_usuario = nombre_usuario;
            fecha = DateTime.Now;
        
        }

        public Contrasena() { }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Aplicacion{

            get { return aplicacion; }
            set { aplicacion = value; }
        }

        public string Nombre_usuario
        {
            get { return nombre_usuario; }
            set { nombre_usuario = value; }
        }
        public string Contraseña
        {
            get { return contrasena; }
            set { contrasena = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
    }
}
