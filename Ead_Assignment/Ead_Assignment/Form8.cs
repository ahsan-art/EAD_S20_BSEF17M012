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
    public partial class Form8 : Form
    {
        String Email;
        public Form8(String email)
        {
            Email = email;
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String password = txtNewPassword.Text;
            if (String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter Password");
            }
            else
            {
                UserPoko user = BO.getUserPokoByEmail(Email);
                user.Password = password;
                int result = BO.Save(user);
                if (result == 0)
                {
                    String applicationBasePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                    String pathToSaveImage = applicationBasePath + @"\assignmentImages\";
                    String Path = pathToSaveImage + user.ImageName;
                    Form5 home = new Form5(user.Name, Path, user.UserID);
                    this.Close();
                    home.Show();
                }

            }
        }
    }
}
