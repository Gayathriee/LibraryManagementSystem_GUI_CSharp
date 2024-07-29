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

namespace Assignment_LibraryManagementSystem_CB010801.Student
{
    public partial class ReturnBooksForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=Gayathrie\SQLEXPRESS;Initial Catalog=users;Integrated Security=True;Encrypt=False");
        //SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g3hap\OneDrive\Documents\MyLibraryDB.mdf;Integrated Security=True;Connect Timeout=30");
        public ReturnBooksForm()
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
            ReturnBookDGV.DataSource = ds.Tables[0];
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
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ReturnNumTb.Text == "" || StudNameTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();

                   
                    if (StudCB.SelectedValue != null)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO ReturnTbl VALUES(@ReturnNum, @StudID, @StudName, @StudDept, @StudPhone, @BookReturned, @IssueDate, @ReturnDate)", Con);

                        cmd.Parameters.AddWithValue("@ReturnNum", ReturnNumTb.Text);
                        cmd.Parameters.AddWithValue("@StudID", StudCB.SelectedValue);
                        cmd.Parameters.AddWithValue("@StudName", StudNameTb.Text);
                        cmd.Parameters.AddWithValue("@StudDept", StudDepTb.Text);
                        cmd.Parameters.AddWithValue("@StudPhone", PhoneTb.Text);
                        cmd.Parameters.AddWithValue("@BookReturned", (BookCb.SelectedItem != null) ? BookCb.SelectedItem.ToString() : string.Empty);
                        cmd.Parameters.AddWithValue("@IssueDate", issueDate.Value);
                        cmd.Parameters.AddWithValue("@ReturnDate", ReturnDate.Value);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Book Successfully Returned!");
                        updateBook();
                        populate();
                    }
                    else
                    {
                        MessageBox.Show("Selected student information is not available.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }


        private void label14_Click(object sender, EventArgs e)
        {
            StudCB.SelectedItem = "";
            StudNameTb.Text = "";
            StudDepTb.Text = "";
            PhoneTb.Text = "";
            BookCb.Text = "";
        }

        private void ReturnBooksForm_Load(object sender, EventArgs e)
        {
            populate();
            FillBook();
        }

        private void StudCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchstddata();
        }
       
    }
}
