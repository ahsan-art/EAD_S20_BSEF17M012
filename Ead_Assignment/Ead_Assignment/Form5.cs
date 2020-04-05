using BAL;
using Entities;
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
    public partial class Form5 : Form
    {
        
        String path;
        int userId;
        String name;
        public Form5(String uname, String ipath, int id)
        {
            InitializeComponent();
            name = uname;
            path = ipath;
            userId = id;
            label1.Text = name;
            pictureBox1.Image = Image.FromFile(path);
        }
        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            UserPoko user = BO.getUserPoko(userId);
            Form2 form = new Form2(user, 0);
            form.ShowDialog();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form = new Form1();
            form.Show();

        }
    }
}
