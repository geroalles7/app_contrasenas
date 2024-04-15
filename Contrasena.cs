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
        int id = 0;


        public Contrasena(string aplicacion, string nombre_usuario, string contrasena)
        {
            id++;
            this.aplicacion = aplicacion;
            this.contrasena = contrasena;
            this.nombre_usuario = nombre_usuario;
        
        }

        public string getAplicacion() {  return aplicacion; }   
        public string getContrasena() {  return contrasena; }   
        public int getId() { return id; }   
        
        public string getNombre_usuario() { return nombre_usuario; }

    }
}
