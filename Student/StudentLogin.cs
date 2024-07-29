using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Assignment_LibraryManagementSystem_CB010801
{
    public partial class StudentLogin : Form
    {
        public StudentLogin()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=Gayathrie\SQLEXPRESS;Initial Catalog=users;Integrated Security=True;Encrypt=False");
        //SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g3hap\OneDrive\Documents\MyLibraryDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void loginForm_clearBtn_Click(object sender, EventArgs e)
        {
            StuloginForm_username.Text = "";
            StuloginForm_password.Text = "";
        }


        //to show the characters as * inivisble
        private void loginForm_showPass_CheckedChanged(object sender, EventArgs e)
        {
            StuloginForm_password.PasswordChar = loginForm_showPass.Checked ? '\0' : '*';
        }

        private void loginForm_loginBtn_Click(object sender, EventArgs e)
        {
           
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from StudentTbl where StudName = '" + StuloginForm_username.Text + "' and StudPass='" + StuloginForm_password.Text + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                StudentMainForm Stumain = new StudentMainForm();
                Stumain.Show();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password!");
            }
            Con.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.Show();
            this.Hide();
        }

       
    }
}
