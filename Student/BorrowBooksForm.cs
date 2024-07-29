using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_LibraryManagementSystem_CB010801.Student
{
    public partial class BorrowBooksForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=Gayathrie\SQLEXPRESS;Initial Catalog=users;Integrated Security=True;Encrypt=False");
        //SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g3hap\OneDrive\Documents\MyLibraryDB.mdf;Integrated Security=True;Connect Timeout=30");
        public BorrowBooksForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void populate()
        {
            Con.Open();
            String query = "select * from BookTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            BorrowBookDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void fetchstddata()
        {
            Con.Open();
            string query = "select * from studentTbl where studID = " + StudCB.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                StudNameTb.Text = dr["StudName"].ToString();
                StudDepTb.Text = dr["StudDep"].ToString();
                PhoneTb.Text = dr["StudPhone"].ToString();

            }
            Con.Close();
        }
        private void FillStudent()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select StudID from StudentTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StudID", typeof(int));
            dt.Load(rdr);
            StudCB.ValueMember = "StudID";
            StudCB.DataSource = dt;
            Con.Close();


        }
        private void updateBookCancellation()
        {
            int Qty, newQty;
            Con.Open();
            string query = "select * from BookTbl where BookName = '" + BookCb.SelectedItem.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qty = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty + 1;
                string query1 = "update BookTbl set Qty=" + newQty + "where BookName='" + BookCb.SelectedItem.ToString() + "';";
                SqlCommand cmd1 = new SqlCommand(@query1, Con);
                cmd1.ExecuteNonQuery();
            }
            Con.Close();
        }
        private void FillBook()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select BookName from BookTbl where Qty>" + 0 + "", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BookName", typeof(string));
            dt.Load(rdr);
            BookCb.ValueMember = "BookName";
            BookCb.DataSource = dt;
            Con.Close();


        }
        private void updateBook()
        {
            int Qty, newQty;
            Con.Open();
            string query = "select * from BookTbl where BookName = '" + BookCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qty = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty - 1;
                string query1 = "update BookTbl set Qty=" + newQty + "where BookName='" + BookCb.SelectedValue.ToString() + "';";
                SqlCommand cmd1 = new SqlCommand(@query1, Con);
                cmd1.ExecuteNonQuery();
            }
            Con.Close();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentMainForm frm = new StudentMainForm();
            frm.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            IssueNumTb.Text = "";
            StudCB.SelectedItem = null;
            StudNameTb.Text = "";
            StudDepTb.Text = "";
            PhoneTb.Text = "";
            BookCb.SelectedItem = null;
            issueDate = null;

        }

        private void BorrowBookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IssueNumTb.Text == "" || StudNameTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                if (IssueNumTb.Text == "" || StudNameTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    DateTime Issuedate = issueDate.Value;
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into IssueTbl values(@IssueNumTb, @StudCB, @StudNameTb, @StudDepTb, @PhoneTb, @BookCb, @IssueDate)", Con);
                    cmd.Parameters.AddWithValue("@IssueNumTb", IssueNumTb.Text);
                    cmd.Parameters.AddWithValue("@StudCB", StudCB.SelectedValue);
                    cmd.Parameters.AddWithValue("@StudNameTb", StudNameTb.Text);
                    cmd.Parameters.AddWithValue("@StudDepTb", StudDepTb.Text);
                    cmd.Parameters.AddWithValue("@PhoneTb", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@BookCb", BookCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@IssueDate", Issuedate);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Successfully Issued!");
                    Con.Close();
                    updateBook();
                    populate();
                }
            }
        }

        private void StudCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchstddata();
        }

        private void BorrowBooksForm_Load(object sender, EventArgs e)
        {
            FillStudent();
            FillBook();
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IssueNumTb.Text == "")
            {
                MessageBox.Show("Enter the Issue Number!");
            }
            else
            {
                Con.Open();
                string query = "delete from IssueTbl where IssueNum = " + IssueNumTb.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Issue Successfully Canceled!");
                Con.Close();
                updateBookCancellation();
                populate();

                // Clear the text boxes
                IssueNumTb.Clear();
                StudNameTb.Clear();
                StudCB.SelectedItem = null;
                BookCb.SelectedItem = null;
                StudDepTb.Clear();
                PhoneTb.Clear();
            }
        }
    }
}
