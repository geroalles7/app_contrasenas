using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app_contraseñas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario=txtUsuario.Text;  
            string contrasena=txtContrasena.Text;   

            if(usuario=="gero" & contrasena == "Gero2002")
            {
                MessageBox.Show("Usuario ingresado con exito");
                VentanaContraseñas vc = new VentanaContraseñas();
                vc.ShowDialog();
               

            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
           
        }
    }
}
