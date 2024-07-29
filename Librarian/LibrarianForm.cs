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
    public partial class LibrarianForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=Gayathrie\SQLEXPRESS;Initial Catalog=users;Integrated Security=True;Encrypt=False");
        //SqlConnection Con = new SqlConnection(@"Data Source=Gayathrie\SQLEXPRESS;Initial Catalog=users;Integrated Security=True;Trust Server Certificate=True");
        public LibrarianForm()
        {
            InitializeComponent();
        }

        private void LibrarianForm_Load(object sender, EventArgs e)
        {
            populate();
        }
        public void populate()
        {
            Con.Open();
            String query = "select * from LibrarianTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            LibrarianDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void button1_Click(object sender, EventArgs e)
        {
                if (LibID.Text == "" || LibName.Text == "" || LibPass.Text == "" || LibPhone.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into LibrarianTbl values(@LibID, @LibName, @LibPass, @LibPhone)", Con);
                    cmd.Parameters.AddWithValue("@LibID", LibID.Text);
                    cmd.Parameters.AddWithValue("@LibName", LibName.Text);
                    cmd.Parameters.AddWithValue("@LibPass", LibPass.Text);
                    cmd.Parameters.AddWithValue("@LibPhone", LibPhone.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Librarian Added Successfully!");
                    Con.Close();
                    populate();
                }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(LibID.Text == "")
            {
                MessageBox.Show("Enter the Librarian ID!");
            }
            else
            {
                Con.Open();
                string query = "delete from LibrarianTbl where LibID = " + LibID.Text + ";";
                SqlCommand cmd = new SqlCommand (query, Con);
                cmd.ExecuteNonQuery ();
                MessageBox.Show ("Librarian successfully Deleted!");
                Con.Close ();
                populate();

                LibID.Clear();
                LibName.Clear();
                LibPass.Clear();
                LibPhone.Clear();
            }
        }

        private void LibrarianDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LibID.Text == "" || LibName.Text == "" || LibPass.Text == "" || LibPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                string query = "update LibrarianTbl set LibName='" + LibName.Text + "',LibPassword='" + LibPhone.Text + "',LibPhone='" + LibPhone.Text + "'where LibID=" + LibID.Text + ";";
                SqlCommand cmd = new SqlCommand(query,Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian Successfully Updated!");
                Con.Close();
                populate();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void LibrarianDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (LibrarianDGV.SelectedRows.Count > 0)
            {
                LibID.Text = LibrarianDGV.SelectedRows[0].Cells[0].Value.ToString();
                LibName.Text = LibrarianDGV.SelectedRows[0].Cells[1].Value.ToString();
                LibPass.Text = LibrarianDGV.SelectedRows[0].Cells[2].Value.ToString();
                LibPhone.Text = LibrarianDGV.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            LibID.Text = "";
            LibName.Text= "";
            LibPass.Text = "";
            LibPhone.Text = "";

        }
    }
}
