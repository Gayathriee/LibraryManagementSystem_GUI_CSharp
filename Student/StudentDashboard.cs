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
    public partial class StudentDashboard : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=Gayathrie\SQLEXPRESS;Initial Catalog=users;Integrated Security=True;Encrypt=False");
        //SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g3hap\OneDrive\Documents\MyLibraryDB.mdf;Integrated Security=True;Connect Timeout=30");
        public StudentDashboard()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void StudentDashboard_Load(object sender, EventArgs e)
        {
            
            
                Con.Open();
                //books
                SqlDataAdapter sda1 = new SqlDataAdapter("select count(*) from BookTbl", Con);
                DataTable dt = new DataTable();
                sda1.Fill(dt);
                Booklbl.Text = dt.Rows[0][0].ToString();
                Con.Close();
            
        }
    }
}
