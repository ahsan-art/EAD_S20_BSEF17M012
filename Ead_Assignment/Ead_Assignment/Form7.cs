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
    public partial class Form7 : Form
    {
        int Code = 0;
        String Email;
        bool flag = true;
        Form2 Form;
        public Form7(int code, String email)
        {
            Code = code;
            Email = email;
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            String code = txtCode.Text;
            if (String.IsNullOrEmpty(code))
            {
                MessageBox.Show("Enter the code first");
            }
            else
            {
                if (Code == int.Parse(code))
                {
                    flag = false;
                    Form8 pForm = new Form8(Email);
                    this.Close();
                    pForm.Show();
                }
                else
                {
                    MessageBox.Show("Code is not correct");
                }
            }
        }
    }
}
