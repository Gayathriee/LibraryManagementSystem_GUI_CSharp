using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;

namespace Assignment_LibraryManagementSystem_CB010801
{
    public partial class LibrarianLoginForm : Form
    {
        public LibrarianLoginForm()
        {
            InitializeComponent();
        }

        private void loginForm_showPass_CheckedChanged(object sender, EventArgs e)
        {
            loginForm_password.PasswordChar = loginForm_showPass.Checked ? '\0' : '*';
        }
        SqlConnection Con = new SqlConnection(@"Data Source=Gayathrie\SQLEXPRESS;Initial Catalog=users;Integrated Security=True;Encrypt=False");
        //SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g3hap\OneDrive\Documents\MyLibraryDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void loginForm_loginBtn_Click(object sender, EventArgs e)
        {
            
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from LibrarianTbl where LibName = '"+ loginForm_username.Text+"' and LibPassword='"+ loginForm_password.Text+ "'",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                MainForm main = new MainForm();
                main.Show();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password!");
            }
            Con.Close();
        }

        private void loginForm_clearBtn_Click(object sender, EventArgs e)
        {
            loginForm_username.Text = "";
            loginForm_password.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void loginForm_signUp_Click(object sender, EventArgs e)
        {
            LibrarianSignUp lbs = new LibrarianSignUp();
            lbs.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            User user = new User();
            user.Show();
        }
    }
}
