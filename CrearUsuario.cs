﻿using System;
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
    public partial class CrearUsuario : Form
    {
        public CrearUsuario()
        {
            InitializeComponent();
            //dejar ventana fija
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.SizeGripStyle = SizeGripStyle.Hide;
            textBox2.PasswordChar = '●';
            txtContraseña2.PasswordChar = '●';
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void CrearUsuario_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '●';
            txtContraseña2.PasswordChar = '●';
        }
    }
}
