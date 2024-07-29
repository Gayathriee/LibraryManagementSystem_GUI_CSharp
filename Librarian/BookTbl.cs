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
    public partial class BookTbl : Form
    {

        public BookTbl()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=Gayathrie\SQLEXPRESS;Initial Catalog=users;Integrated Security=True;Encrypt=False");
        //SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g3hap\OneDrive\Documents\MyLibraryDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void BookTbl_Load(object sender, EventArgs e)
        {
            populate();
        }

        //to display data in the dgv
        public void populate()
        {
            Con.Open();
            String query = "select * from BookTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int priceValue, qtyValue;
            bool isValid = true;

            if (Bookname.Text == "" || author.Text == "" || publisher.Text == "" || price.Text == "" || Qty.Text == "")
            {
                MessageBox.Show("Missing Information");
                isValid = false;
            }
            else
            {
                try
                {
                    priceValue = int.Parse(price.Text);
                    qtyValue= int.Parse(Qty.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please enter valid integers for Price and Quantity.");
                    isValid = false;
                }
            }

            if (isValid)
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into BookTbl (BookName, Author, Publisher, Price, Qty) values (@Bookname, @author, @publisher, @price, @Qty)", Con);
                cmd.Parameters.AddWithValue("@BookName", Bookname.Text);
                cmd.Parameters.AddWithValue("@Author", author.Text);
                cmd.Parameters.AddWithValue("@Publisher", publisher.Text);
                cmd.Parameters.AddWithValue("@Price", price.Text);
                cmd.Parameters.AddWithValue("@Qty", Qty.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Successfully Added!");
                Con.Close();
                populate();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }


        //delete button
            private void button3_Click(object sender, EventArgs e)
        {
            if (Bookname.Text == "")
            {
                MessageBox.Show("Enter the Book Name!");
            }
            else
            {
                Con.Open();
                string query = "delete from BookTbl where BookName = '" + Bookname.Text + "';";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book has been successfully Deleted!");
                Con.Close();
                populate();
            }
        }

        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }


        //edit button
        private void edit_Click(object sender, EventArgs e)
        {
            if (Bookname.Text == "" || author.Text == "" || publisher.Text == "" || price.Text == "" || Qty.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                string query = "update BookTbl set Author='" + author.Text + "',Publisher='" + publisher.Text + "',price='" + int.Parse(price.Text) + "' where BookName='" + Bookname.Text + "';";//BookName is the primary key
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Books Successfully Updated!");
                Con.Close();
                populate();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        //previous
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }


        //selecting values in dgv to the text boxes
        private void BookDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (BookDGV.SelectedRows.Count > 0)
            {
                
                Bookname.Text = BookDGV.SelectedRows[0].Cells[0].Value.ToString();
                author.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
                publisher.Text = BookDGV.SelectedRows[0].Cells[2].Value.ToString();
                price.Text = BookDGV.SelectedRows[0].Cells[3].Value.ToString();
                Qty.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            }
        }


        //clear button
        private void label6_Click(object sender, EventArgs e)
        {
            Bookname.Text = "";
            author.Text = "";
            publisher.Text = "";
            price.Text = "";
            Qty.Text = "";
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
