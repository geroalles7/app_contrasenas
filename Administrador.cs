using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_contraseñas
{
    internal class Administrador
    {
        List<Contrasena> lista = new List<Contrasena>();    
        public Administrador() { }
        public void CrearContrasena(string aplicacion, string nombre_usuario, string contrasena)
        {
            Contrasena con = new Contrasena(aplicacion, nombre_usuario, contrasena);

            lista.Add(con); 
        }

        public void EditarContrasena(string aplicacion, string nombre_usuario, string contrasena)
        {

        }
        public List<Contrasena> GetMiLista()
        {
            return lista;
        }
        public void SetMiLista(List<Contrasena> nuevaLista)
        {
            lista = nuevaLista;
        }


    }
}
