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

namespace Assignment_LibraryManagementSystem_CB010801
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=Gayathrie\SQLEXPRESS;Initial Catalog=users;Integrated Security=True;Trust Server Certificate=True");
        //SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g3hap\OneDrive\Documents\MyLibraryDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void pictureBox1_Click(object sender, EventArgs e)
        {
           // this.Hide();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            Con.Open();
            //books
            SqlDataAdapter sda1 = new SqlDataAdapter("select count(*) from BookTbl",Con);
            DataTable dt = new DataTable();
            sda1.Fill(dt);
            Booklbl.Text = dt.Rows[0][0].ToString();
            //students
            SqlDataAdapter sda2 = new SqlDataAdapter("select count(*) from StudentTbl", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            Studentlbl.Text = dt2.Rows[0][0].ToString();
            //Return
            SqlDataAdapter sda3 = new SqlDataAdapter("select count(*) from ReturnTbl", Con);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            Returnlbl.Text = dt3.Rows[0][0].ToString();
            //Librarians
            SqlDataAdapter sda4 = new SqlDataAdapter("select count(*) from LibrarianTbl", Con);
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4);
            Librarianlbl.Text = dt4.Rows[0][0].ToString();
            //issue
            SqlDataAdapter sda5 = new SqlDataAdapter("select count(*) from IssueTbl", Con);
            DataTable dt5 = new DataTable();
            sda5.Fill(dt5);
            Issuelbl.Text = dt5.Rows[0][0].ToString();
            Con.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }
    }
}
