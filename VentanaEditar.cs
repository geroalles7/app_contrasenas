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
    public partial class VentanaEditar : Form
    {
        public VentanaEditar()
        {
            InitializeComponent();
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        public string GenerarContraseña()
        {
            const string CaracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+[{]}|;:',<.>/?";
            const int LongitudContraseña = 16; // Longitud de la contraseña deseada
            Random random = new Random();
            StringBuilder contraseña = new StringBuilder();

            // Generar la contraseña con caracteres aleatorios
            while (contraseña.Length < LongitudContraseña)
            {
                int indiceCaracter = random.Next(CaracteresPermitidos.Length);
                contraseña.Append(CaracteresPermitidos[indiceCaracter]);
            }

            return contraseña.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string nuevaContraseña = GenerarContraseña();

                txtContraseña.Text = nuevaContraseña;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar la contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
