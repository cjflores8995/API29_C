using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encriptor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncriptar_Click(object sender, EventArgs e)
        {
            txtResultado.Text = Util.Encriptar(txtTexto.Text, txtSemilla.Text);
        }

        private void btnDesencriptar_Click(object sender, EventArgs e)
        {
            txtResultado.Text = Util.Desencriptar(txtTexto.Text, txtSemilla.Text);
        }
    }
}
