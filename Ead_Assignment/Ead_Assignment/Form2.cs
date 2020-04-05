using BAL;
using Entities;
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

namespace Ead_Assignment
{
    public partial class Form2 : Form
    {
        UserPoko user;
        int user_Id;
        bool pflag = false;
        public Form2(UserPoko suser, int userId)
        {
            user = suser;
            user_Id = userId;
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            pflag = true;
            var result = openFileDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var path = openFileDialog1.FileName;
                imageBox.Image = Image.FromFile(path);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            String name = txtName.Text;
            String login = txtLogin.Text;
            String email = txtEmail.Text;
            String password = txtPassword.Text;
            String gender = listGender.Text;
            String address = txtAddress.Text;
            String age = numAge.Value.ToString();
            String NIC = numNIC.Text;
            bool cricket = checkCricket.Checked;
            bool hockey = checkHockey.Checked;
            bool chess = checkChess.Checked;
            DateTime DOB = dob.Value;

            DateTime createdOn;
            if (user == null && user_Id == 0)
            {
                createdOn = DateTime.Now;
            }
            else if (user != null)
            {
                createdOn = user.createdOn;
            }
            else
            {
                UserPoko userFromAdmin = BO.getUserPoko(user_Id);
                createdOn = userFromAdmin.createdOn;
            }
            String createdBy = login;
            String modifiedBy = login;
            DateTime modifiedOn = createdOn;
            bool isActive = true;
            String imgName;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(age) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(address) || imageBox.Image == null)
            {
                MessageBox.Show("Some fields are empty");
            }
            else
            {
                String applicationBasePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                String pathToSaveImage = applicationBasePath + @"\assignmentImages\";
                String imgPath;


                if (user_Id > 0)
                {
                    if (pflag == true)
                    {
                        imgName = Guid.NewGuid().ToString() + ".jpg";
                        imgPath = pathToSaveImage + imgName;
                        imageBox.Image.Save(imgPath);
                    }
                    else
                    {
                        UserPoko user1 = BO.getUserPoko(user_Id);
                        imgName = user1.ImageName;
                        imgPath = pathToSaveImage + imgName;
                    }
                }
                else
                {
                    imgName = Guid.NewGuid().ToString() + ".jpg";
                    imgPath = pathToSaveImage + imgName;
                    imageBox.Image.Save(imgPath);
                }
                gender = gender.Substring(0, 1);
                UserPoko userPoko = new UserPoko();

                userPoko.Name = name;
                userPoko.Login = login;
                userPoko.Password = password;
                userPoko.Email = email;
                userPoko.Gender = gender;
                userPoko.Address = address;
                userPoko.Age = int.Parse(age);
                userPoko.NIC = NIC;
                userPoko.DOB = DOB;
                userPoko.isCricket = cricket;
                userPoko.isHockey = hockey;
                userPoko.Chess = chess;
                userPoko.ImageName = imgName;
                userPoko.createdOn = createdOn;
                userPoko.createdBy = createdBy;
                userPoko.modifiedOn = modifiedOn;
                userPoko.modifiedBy = modifiedBy;
                userPoko.isActive = isActive;


                UserPoko userPoko1 = null;


                if (user_Id > 0)
                {
                    userPoko.UserID = user_Id;
                    userPoko1 = BO.validateUser(login, password, email, user_Id, 3);                  
                }
                else if (user != null)
                {
                    userPoko.UserID = user.UserID;
                    userPoko1 = BO.validateUser(login, password, email, userPoko.UserID, 3);
                }
                else
                {
                    userPoko1 = BO.validateUser(login, password, email, 0, 2);
                }


                if (userPoko1 == null)
                {
                    if (user != null)
                    {
                        userPoko.modifiedOn = DateTime.Now;

                    }
                    else if (user_Id > 0)
                    {
                        userPoko.modifiedOn = DateTime.Now;
                        userPoko.modifiedBy = "admin";
                    }
                    int res = BO.Save(userPoko);
                    if (res > 0)
                    {
                        userPoko.UserID = res;
                    }
                    if (user_Id > 0)
                    {
                        this.Close();
                         Form6 ahome = new Form6();
                        ahome.Show();
                    }
                    else
                    {

                        Form5 home = new Form5(name, imgPath, userPoko.UserID);
                        this.Close();
                        var form = Application.OpenForms["Form1"];
                        form.Hide();
                        var form1 = Application.OpenForms["Form5"];
                        if (form1 != null)
                        {
                            form1.Close();
                        }
                        home.Show();
                    }
                }
                else
                {
                    if (userPoko1 != null)
                    {
                        MessageBox.Show("Login already exists");
                    }
                }

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            String applicationBasePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            System.IO.Directory.CreateDirectory(applicationBasePath + @"\assignmentImages\");
            if (user_Id > 0)
            {
               
                UserPoko user = BO.getUserPoko(user_Id);
                txtName.Text = user.Name;
                txtLogin.Text = user.Login;
                txtPassword.Text = user.Password;
                txtAddress.Text = user.Address;
                numAge.Text = user.Age.ToString();
                numNIC.Text = user.NIC;
                dob.Value = user.DOB;
                txtEmail.Text = user.Email;

                if (user.isCricket == true)
                {
                    checkCricket.CheckState = CheckState.Checked;
                }
                else if (user.isHockey == true)
                {
                    checkHockey.CheckState = CheckState.Checked;
                }
                else if (user.Chess == true)
                {
                    checkChess.CheckState = CheckState.Checked;
                }
                if (user.Gender.Equals("M"))
                {
                    listGender.Text = "Male";
                }
                else
                {
                    listGender.Text = "Female";
                }
                String imgPath = applicationBasePath + @"\assignmentImages\" + user.ImageName;
                imageBox.Image = Image.FromFile(imgPath);
            }
            if (user != null)
            {
                txtName.Text = user.Name;
                txtLogin.Text = user.Login;
                txtPassword.Text = user.Password;
            }
            listGender.Items.Add("Male");
            listGender.Items.Add("Female");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            if (user_Id > 0)
            {
                Form6 aHome= new Form6();
                aHome.Show();
            }
        }
    }

}
