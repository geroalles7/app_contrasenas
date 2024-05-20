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
            string nombre = txtUsuario.Text;
            string contraseña = txtContrasena.Text;

            // Obtener todos los usuarios de la base de datos
            List<Usuario> usuarios = ad.GetUsuarios();

            // Verificar si existe algún usuario con las credenciales ingresadas
            bool usuarioValido = false;
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Nombre == nombre && usuario.Contraseña == contraseña)
                {
                    usuarioValido = true;
                    break;
                }
            }

            if (usuarioValido)
            {
                MessageBox.Show("Usuario ingresado con éxito");
                VentanaContraseñas vc = new VentanaContraseñas();
                vc.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
        }

        Administrador ad =new Administrador();   
        private void crearUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CrearUsuario cu=new CrearUsuario(); 
            if(cu.ShowDialog()== DialogResult.OK) 
            {
                string nombre = cu.textBox1.Text;
                string contraseña = cu.textBox2.Text;
                ad.AgregarUsuario(nombre, contraseña);

               
            }
        }
    }
}
