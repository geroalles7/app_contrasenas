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
        private Administrador dbHelper = new Administrador();
        private DataTable dataTable;
       

        public VentanaContraseñas()
        {
            InitializeComponent();
            
        }
        Administrador ad= new Administrador();

        private void btnCrear_Click(object sender, EventArgs e)
        {
            VentanaCrear ventanaCrear = new VentanaCrear();
            if (ventanaCrear.ShowDialog() == DialogResult.OK)
            {
                
                ad.CrearContrasena(ventanaCrear.txtApp.Text, ventanaCrear.txtUsuario.Text, ventanaCrear.txtContraseña.Text, DateTime.Now);
                dataGridView1.Rows.Add(ad.getTamañoLista(),ventanaCrear.txtApp.Text, ventanaCrear.txtUsuario.Text, ventanaCrear.txtContraseña.Text, DateTime.Now);
            }
        }
       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay una fila seleccionada
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Obtener el índice de la fila seleccionada
                    int index = dataGridView1.SelectedRows[0].Index;


                    // Eliminar la fila correspondiente de la lista en el Administrador
                    ad.EliminarContrasena(index);

                    // Eliminar la fila del DataGridView
                    dataGridView1.Rows.RemoveAt(index);

                }
                else
                {
                    MessageBox.Show("Por favor, seleccione una fila para eliminar.", "Mensaje");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener el índice de la fila seleccionada
                int index = dataGridView1.SelectedRows[0].Index;

                // Obtener los datos actuales de la fila seleccionada
                DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];
                string app = filaSeleccionada.Cells[1].Value.ToString(); // Asumiendo que App está en la primera columna (0)
                string usuario = filaSeleccionada.Cells[2].Value.ToString(); // Asumiendo que Usuario está en la segunda columna (1)
                string contraseña = filaSeleccionada.Cells[3].Value.ToString(); // Asumiendo que Contraseña está en la tercera columna (2)

                // Crear y mostrar el formulario de edición
                VentanaEditar ventanaEditar = new VentanaEditar();
                ventanaEditar.txtApp.Text = app;
                ventanaEditar.txtUsuario.Text = usuario;
                ventanaEditar.txtContraseña.Text = contraseña;

                if (ventanaEditar.ShowDialog() == DialogResult.OK)
                {
                    // Actualizar la lista en el objeto Administrador
                    ad.ActualizarContrasena(index, ventanaEditar.txtApp.Text, ventanaEditar.txtUsuario.Text, ventanaEditar.txtContraseña.Text);

                    // Actualizar los datos en la fila seleccionada
                    dataGridView1.Rows[index].Cells[0].Value = ventanaEditar.txtApp.Text;
                    dataGridView1.Rows[index].Cells[1].Value = ventanaEditar.txtUsuario.Text;
                    dataGridView1.Rows[index].Cells[2].Value = ventanaEditar.txtContraseña.Text;
                    dataGridView1.Rows[index].Cells[3].Value = DateTime.Now.ToString(); // Actualizar la fecha

                    // No es necesario volver a enlazar la lista al DataGridView
                    // No establecer dataGridView1.DataSource = ad.GetMiLista();

                    // Opcional: Limpiar la selección
                    dataGridView1.ClearSelection();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para modificar.", "Mensaje");
            }
        }

        private void VentanaContraseñas_Load_1(object sender, EventArgs e)
        {
            dataTable = dbHelper.GetPasswords();
            dataGridView1.DataSource = dataTable;

        }
    }
}
