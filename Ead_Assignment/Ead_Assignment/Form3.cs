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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String name = txtLogin.Text;
            String password = txtPassword.Text;
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Empty inputs");
            }
            else
            {
                UserPoko user = BO.validateUser(name, password, "", 0, 1);
                if (user == null)
                {
                    MessageBox.Show("Email or password is not valid!");
                }
                else
                {
                    String applicationBasePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                    String pathToSaveImage = applicationBasePath + @"\assignmentImages\";
                    String imgPath = pathToSaveImage + user.ImageName;
                    Form5 home= new Form5(user.Name, imgPath, user.UserID);
                    this.Hide();
                    var form = Application.OpenForms["Form1"];
                    if (form != null)
                    {
                        form.Hide();
                    }
                    home.Show();


                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form = new Form1();
            form.Show();
        }
    }
}
