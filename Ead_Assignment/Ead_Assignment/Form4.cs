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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String aLogin = txtLogin.Text;
            String aPassword = txtPassword.Text;
            if (String.IsNullOrEmpty(aLogin) || String.IsNullOrEmpty(aPassword))
            {
                MessageBox.Show("Fields are empty");
            }
            else
            {
                AdminPoko admin = new AdminPoko();
                admin.Login = aLogin;
                admin.Password = aPassword;
                int records =BO.validateAdmin(admin);
                if (records == 1)
                { 
                    var hform = Application.OpenForms["Form1"];
                    hform.Hide();
                    this.Close();
                    Form6 aForm = new Form6();
                    aForm.Show();
                }
                else
                {
                    MessageBox.Show("Login or password is not correct");
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
