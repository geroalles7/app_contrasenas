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
    public partial class ValidarUsuario : Form
    {
        public ValidarUsuario()
        {
            InitializeComponent();
            textBox2.PasswordChar = '●';
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ValidarUsuario_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '●';
        }
    }
}
