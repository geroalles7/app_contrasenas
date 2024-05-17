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
        private Administrador ad = new Administrador(); 
        private DataTable dataTable;

        public VentanaContraseñas()
        {
            InitializeComponent();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            VentanaCrear ventanaCrear = new VentanaCrear();
            if (ventanaCrear.ShowDialog() == DialogResult.OK)
            {
                
                ad.CrearContrasena(ventanaCrear.txtApp.Text, ventanaCrear.txtUsuario.Text, ventanaCrear.txtContraseña.Text, DateTime.Now);

                
                ActualizarDataTable();

                
                dataGridView1.ClearSelection();
            }
        }

        private void txtEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    
                    int index = dataGridView1.SelectedRows[0].Index;
                    
                    int id = (int)dataGridView1.Rows[index].Cells["id"].Value;

                    
                    ad.EliminarContrasena(id);

                    ActualizarDataTable();

                    
                    dataGridView1.ClearSelection();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione una fila para eliminar.", "Mensaje");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
               
                int id = (int)dataGridView1.Rows[index].Cells["id"].Value;

                
                DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];
                string app = filaSeleccionada.Cells["app"].Value.ToString();
                string usuario = filaSeleccionada.Cells["usuario"].Value.ToString();
                string contraseña = filaSeleccionada.Cells["contraseña"].Value.ToString();

                
                VentanaEditar ventanaEditar = new VentanaEditar();
                ventanaEditar.txtApp.Text = app;
                ventanaEditar.txtUsuario.Text = usuario;
                ventanaEditar.txtContraseña.Text = contraseña;

                if (ventanaEditar.ShowDialog() == DialogResult.OK)
                {
                    
                    ad.ActualizarContrasena(id, ventanaEditar.txtApp.Text, ventanaEditar.txtUsuario.Text, ventanaEditar.txtContraseña.Text);

                    
                    ActualizarDataTable();

                    
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
            
            ActualizarDataTable();
        }

        private void ActualizarDataTable()
        {
            dataTable = ad.GetPasswords();

            
            dataGridView1.Columns.Clear();

            
            dataGridView1.DataSource = dataTable;

           
        }

    }
}
