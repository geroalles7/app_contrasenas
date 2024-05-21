using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_contraseñas
{
    internal class Usuario
    {
        int id=0;
        string nombre;
        string contraseña;

        public Usuario(int id,string nombre, string contraseña) 
        {
            this.id = id;
            this.nombre = nombre;
            this.contraseña = contraseña;
        }
        public Usuario()
        {

        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get;set;
        }

        public string Contraseña
        {
            get; set;
        }
    }
}
