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
    public partial class ReturnForm : Form
    {
        public ReturnForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=Gayathrie\SQLEXPRESS;Initial Catalog=users;Integrated Security=True;Encrypt=False");
        //SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g3hap\OneDrive\Documents\MyLibraryDB.mdf;Integrated Security=True;Connect Timeout=30");
        public void populate()
        {
            Con.Open();
            String query = "select * from IssueTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            issueBookDGV.DataSource = ds.Tables[0];
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
        public void populatereturn()
        {
            Con.Open();
            String query = "select * from ReturnTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReturnedBookDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void fetchstddata()
        {
            Con.Open();
            string query = "select * from StudentTbl where studID = " + StudCB.SelectedValue.ToString() + "";
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
        private void ReturnForm_Load(object sender, EventArgs e)
        {
            populate();
            populatereturn();
            FillBook();
        }

        private void issueBookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void updateBook()
        {
            int Qty, newQty;
            Con.Open();
            string query = "SELECT * FROM BookTbl WHERE BookName = '" + BookCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Qty = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty + 1;

                string query1 = "UPDATE BookTbl SET Qty = " + newQty + " WHERE BookName = '" + BookCb.SelectedValue.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(@query1, Con);
                cmd1.ExecuteNonQuery();
            }

            Con.Close();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO ReturnTbl VALUES(@ReturnNum, @StudID, @StudName, @StudDept, @StudPhone, @BookReturned, @IssueDate, @ReturnDate)", Con))
                    {
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
                    }
                    Con.Close();

                   
                    updateBook();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void issueBookDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (issueBookDGV.SelectedRows.Count > 0)
            {
                // IssueNumTb.Text = issueBookDGV.SelectedRows[0].Cells[0].Value.ToString();
                StudCB.SelectedItem = issueBookDGV.SelectedRows[0].Cells[1].Value.ToString();
                StudNameTb.Text = issueBookDGV.SelectedRows[0].Cells[2].Value.ToString();
                StudDepTb.Text = issueBookDGV.SelectedRows[0].Cells[3].Value.ToString();
                PhoneTb.Text = issueBookDGV.SelectedRows[0].Cells[4].Value.ToString();
                BookCb.Text = issueBookDGV.SelectedRows[0].Cells[5].Value.ToString();
            }
        }

        private void ReturnedBookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }


        //print button
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap,0,0);
        }
        Bitmap bitmap;
        private void button2_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            this.Controls.Add(panel);
            Graphics graphics = panel.CreateGraphics();
            Size size = this.ClientSize;
            bitmap = new Bitmap(size.Width, Size.Height, graphics);
            graphics = Graphics.FromImage(bitmap);
            Point point = PointToScreen(panel.Location);
            graphics.CopyFromScreen(point.X, point.Y, 0, 0, size);
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }


        //clear button
        private void label14_Click(object sender, EventArgs e)
        {
           
            StudCB.SelectedItem = "";
            StudNameTb.Text = "";
            StudDepTb.Text = "";
            PhoneTb.Text = "";
            BookCb.Text = "";
        }

        private void StudCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchstddata();
        }
    }
}
