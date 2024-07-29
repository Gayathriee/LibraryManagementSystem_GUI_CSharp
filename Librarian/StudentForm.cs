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
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=Gayathrie\SQLEXPRESS;Initial Catalog=users;Integrated Security=True;Encrypt=False");
        //SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g3hap\OneDrive\Documents\MyLibraryDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();

        }
        public void populate()
        {
            Con.Open();
            String query = "select * from StudentTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            StudentDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm(); 
            main.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (StudID.Text == "" || StudName.Text == "" || StudDep.Text == "" || StudSem.Text == "" || StudPhone.Text == "" || StudPass.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into StudentTbl (StudID, StudName, StudDep, StudSem, StudPhone,StudPass) values (@StudID, @StudName, @StudDep, @StudSem, @StudPhone,@StudPass)", Con);
                cmd.Parameters.AddWithValue("@StudID", StudID.Text);
                cmd.Parameters.AddWithValue("@StudName", StudName.Text);
                cmd.Parameters.AddWithValue("@StudDep", StudDep.Text);
                cmd.Parameters.AddWithValue("@StudSem", StudSem.Text);
                cmd.Parameters.AddWithValue("@StudPhone", StudPhone.Text);
                cmd.Parameters.AddWithValue("@StudPass", StudPass.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Added Successfully!");
                Con.Close();
                populate();
            }
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            populate();
        }


        //delete button
        private void button3_Click(object sender, EventArgs e)
        {
            if (StudID.Text == "")
            {
                MessageBox.Show("Enter the Student ID!");
            }
            else
            {
                Con.Open();
                string query = "delete from StudentTbl where StudID = " + StudID.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student successfully Deleted!");
                Con.Close();
                populate();

                StudDep.Clear();
                StudID.Clear();
                StudName.Clear();
                StudPass.Clear();
                StudSem.SelectedItem = null;
                StudPhone.Clear();
            }
        }

        private void StudentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (StudID.Text == "" || StudName.Text == "" || StudDep.Text == "" || StudSem.Text == "" || StudPhone.Text == "" || StudPass.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                string query = "update StudentTbl set StudID=@StudID, StudName=@StudName, StudDep=@StudDep, StudSem=@StudSem, StudPhone=@StudPhone where StudPass=@StudPass";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@StudID", StudID.Text);
                cmd.Parameters.AddWithValue("@StudName", StudName.Text);
                cmd.Parameters.AddWithValue("@StudDep", StudDep.Text);
                cmd.Parameters.AddWithValue("@StudSem", StudSem.Text);
                cmd.Parameters.AddWithValue("@StudPhone", StudPhone.Text);
                cmd.Parameters.AddWithValue("@StudPass", StudPass.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Successfully Updated!");
                Con.Close();
                populate();
            }
        }

        private void StudName_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void StudentDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (StudentDGV.SelectedRows.Count > 0)
            {
                StudID.Text = StudentDGV.Rows[0].Cells[0].Value.ToString();
                StudName.Text = StudentDGV.Rows[0].Cells[1].Value.ToString();
                StudDep.Text = StudentDGV.Rows[0].Cells[2].Value.ToString();
                StudSem.SelectedItem = StudentDGV.Rows[0].Cells[3].Value.ToString();
                StudPhone.Text = StudentDGV.Rows[0].Cells[4].Value.ToString();
                StudPass.Text = StudentDGV.Rows[0].Cells[5].Value.ToString();

            }
        }


        //clear button
        private void label10_Click(object sender, EventArgs e)
        {
            StudID.Text = "";
            StudName.Text = "";
            StudDep.Text = "";
            StudSem.SelectedItem = null;
            StudPhone.Text = "";
            StudPass.Text = "";
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
