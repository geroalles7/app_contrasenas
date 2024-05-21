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
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true; // Si deseas permitir minimizar, de lo contrario, configúralo como false
            this.SizeGripStyle = SizeGripStyle.Hide;

        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        Administrador ad = new Administrador();
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text;
            string contraseña = txtContrasena.Text;

            
            List<Usuario> usuarios = ad.GetUsuarios();

            Usuario usuario = usuarios.FirstOrDefault(u => u.Nombre == nombreUsuario &&  u.Contraseña == contraseña);

            if (usuario != null)
            {
                int usuario_id = usuario.Id;
                VentanaContraseñas ventanaContraseñas = new VentanaContraseñas(usuario_id);
                MessageBox.Show("Usuario ingresado con exito !", "Usuario ingresado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ventanaContraseñas.Show();
                this.Hide();
                
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
