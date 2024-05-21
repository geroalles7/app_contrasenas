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
    public partial class VentanaCrear : Form
    {
        public VentanaCrear()
        {
            InitializeComponent();
        }

        public string App
        {
            get { return txtApp.Text; }
        }

        public string Usuario
        {
            get { return txtUsuario.Text; }
        }

        public string Contraseña
        {
            get { return txtContraseña.Text; }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        Administrador ad = new Administrador();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string nuevaContraseña = ad.GenerarContraseña();

                txtContraseña.Text = nuevaContraseña;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar la contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
