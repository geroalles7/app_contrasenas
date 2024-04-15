using Microsoft.Win32;
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
    public partial class VentanaContraseñas : Form
    {
        public VentanaContraseñas()
        {
            InitializeComponent();
            InitializeDataTable();
            InitializeDataGridView();
        }
        Administrador ad= new Administrador();

        private void btnCrear_Click(object sender, EventArgs e)
        {
            VentanaCrear ventanaCrear = new VentanaCrear();
            if (ventanaCrear.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Add(ventanaCrear.txtApp.Text, ventanaCrear.txtUsuario.Text, ventanaCrear.txtContraseña.Text);
                
                ad.CrearContrasena(ventanaCrear.txtApp.Text, ventanaCrear.txtUsuario.Text, ventanaCrear.txtContraseña.Text);
                
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener el índice de la fila seleccionada
                int index = dataGridView1.SelectedRows[0].Index;

                // Eliminar la fila correspondiente de la lista
                ad.GetMiLista().RemoveAt(index);

                // Volver a enlazar la lista al DataGridView para actualizar la vista
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = ad.GetMiLista();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para eliminar.", "Mensaje");
            }
        }
    }
}
