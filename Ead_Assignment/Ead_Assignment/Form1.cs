using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ead_Assignment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            
            Form4 form = new Form4();
            form.ShowDialog();
            
        }

        private void btnExistingUser_Click(object sender, EventArgs e)
        {
          
            Form3 form = new Form3();
            form.ShowDialog();
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            
            Form2 form = new Form2(null,0);
            form.ShowDialog();

        }
    }
}
