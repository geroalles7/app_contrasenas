using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private int usuario_id;
        private bool regresarAForm1 = false;
        public VentanaContraseñas(int usuario_id)
        {
            InitializeComponent();
            this.usuario_id = usuario_id;
            this.FormClosed += new FormClosedEventHandler(VentanaContraseñas_FormClosed);

            dataGridView1.ReadOnly = true;

            //exportar PDF
            this.pDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pDFToolStripMenuItem.Text = "Exportar a PDF";
            this.pDFToolStripMenuItem.Click += new System.EventHandler(this.pDFToolStripMenuItem_Click);

            //exportar CSV
            this.cSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVToolStripMenuItem.Text = "Exportar a CSV";
            this.cSVToolStripMenuItem.Click += new System.EventHandler(this.cSVToolStripMenuItem_Click);

            //dejar ventana fija
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true; 
            this.SizeGripStyle = SizeGripStyle.Hide;


           

        }
        private void VentanaContraseñas_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!regresarAForm1)
            {
                Application.Exit();
            }

        }


        private void btnCrear_Click(object sender, EventArgs e)
        {
            VentanaCrear ventanaCrear = new VentanaCrear();
            if (ventanaCrear.ShowDialog() == DialogResult.OK)
            {
                ad.CrearContrasena(ventanaCrear.App, ventanaCrear.Usuario, ventanaCrear.Contraseña, DateTime.Now, this.usuario_id);

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
                    DialogResult resultado = MessageBox.Show("¿Estás seguro que desea eliminar esta contraseña?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        int index = dataGridView1.SelectedRows[0].Index;

                        int id = (int)dataGridView1.Rows[index].Cells["id"].Value;


                        ad.EliminarContrasena(id);

                        ActualizarDataTable();


                        dataGridView1.ClearSelection();
                    }

                }
                else
                {
                    
                    MessageBox.Show("Por favor, seleccione una fila para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    ad.ActualizarContrasena(id, ventanaEditar.txtApp.Text, ventanaEditar.txtUsuario.Text, ventanaEditar.txtContraseña.Text, DateTime.Now);


                    ActualizarDataTable();


                    dataGridView1.ClearSelection();
                }
            }
            else
            {
                
                MessageBox.Show("Por favor, seleccione una fila para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void VentanaContraseñas_Load_1(object sender, EventArgs e)
        {

            ActualizarDataTable();
            
        }

        private void ActualizarDataTable()
        {
            dataTable = ad.GetPasswords(this.usuario_id);


            dataGridView1.Columns.Clear();


            dataGridView1.DataSource = dataTable;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void exportarCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("¿Estás seguro que deseas volver al menú?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                regresarAForm1 = true;
                Form1 form1 = new Form1();
                form1.Show();
                this.Close();
            }
        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModificarUsuario mu = new ModificarUsuario();
            Usuario usuario = ad.GetUsuario(this.usuario_id);
            mu.textBox1.Text = usuario.Nombre;
            mu.textBox2.Text = usuario.Contraseña;

            if (mu.ShowDialog() == DialogResult.OK)
            {

                string nombre = mu.textBox1.Text;
                string contraseña = mu.textBox2.Text;
                ad.ActualizarUsuario(this.usuario_id, nombre, contraseña);
            }
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Crear un SaveFileDialog para permitir al usuario especificar la ubicación y el nombre del archivo PDF
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.Title = "Guardar como PDF";
            DialogResult result = MessageBox.Show("¿Estás seguro que deseas exportar las contraseñas en PDF ?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Crear un documento PDF
                    Document pdfDoc = new Document(PageSize.A4);

                    try
                    {
                        // Crear un escritor para el documento PDF
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                        pdfDoc.Open();

                        // Crear una fuente para el documento PDF
                        BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 12);

                        // Crear una tabla con el número de columnas del DataGridView
                        PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);

                        // Añadir las cabeceras de columna al PDF
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, font));
                            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                            pdfTable.AddCell(cell);
                        }

                        // Añadir las filas del DataGridView al PDF
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (cell.Value != null)
                                {
                                    pdfTable.AddCell(new Phrase(cell.Value.ToString(), font));
                                }
                            }
                        }

                        // Añadir la tabla al documento PDF
                        pdfDoc.Add(pdfTable);
                        
                        MessageBox.Show("Archivo creado con exito !", "Archivo creado", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    catch (Exception ex)
                    {
                       
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    finally
                    {
                        // Cerrar el documento PDF
                        pdfDoc.Close();
                    }
                }

            }
        }

        private void cSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files|*.csv";
            saveFileDialog.Title = "Guardar como CSV";

            DialogResult result = MessageBox.Show("¿Estás seguro que deseas exportar las contraseñas en CSV (Excel) ?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                        {
                            // Escribir las cabeceras de las columnas
                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                sw.Write($"\"{dataGridView1.Columns[i].HeaderText}\"");
                                if (i < dataGridView1.Columns.Count - 1)
                                {
                                    sw.Write(";");
                                }
                            }
                            sw.WriteLine();

                            // Escribir las filas de datos
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                    {
                                        if (row.Cells[i].Value != null)
                                        {
                                            string cellValue = row.Cells[i].Value.ToString();

                                            // Si el valor contiene comillas, escaparlas duplicándolas
                                            cellValue = cellValue.Replace("\"", "\"\"");

                                            // Si el valor contiene comas, rodearlo con comillas
                                            if (cellValue.Contains(";"))
                                            {
                                                cellValue = $"\"{cellValue}\"";
                                            }

                                            sw.Write(cellValue);
                                        }

                                        if (i < dataGridView1.Columns.Count - 1)
                                        {
                                            sw.Write(";");
                                        }
                                    }
                                    sw.WriteLine();
                                }
                            }
                        }

                        MessageBox.Show("Datos exportados con éxito.", "Exportar a CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al exportar a CSV: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "contraseña")
            {
                    // Solicitar nombre y contraseña del usuario
                    ValidarUsuario vu =new ValidarUsuario();
                
       
                    if (vu.ShowDialog() == DialogResult.OK)
                    {
                        string usuario = vu.textBox1.Text;
                        string contraseña = vu.textBox2.Text;

                        Usuario usuarioActual = ad.GetUsuario(this.usuario_id); // Obtener el usuario actual

                        if (usuarioActual != null && usuarioActual.Nombre == usuario && usuarioActual.Contraseña == contraseña)
                        {
                            // Desbloquear y mostrar la contraseña real
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = "Unlocked";
                            dataGridView1.InvalidateCell(e.ColumnIndex, e.RowIndex);
                        }
                        else
                        {
                            MessageBox.Show("Credenciales incorrectas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "contraseña" && e.Value != null)
            {
                if (!dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag?.ToString().Equals("Unlocked") ?? true)
                {
                    e.Value = new string('•', e.Value.ToString().Length);
                }
            }
        }
        
    }
}

