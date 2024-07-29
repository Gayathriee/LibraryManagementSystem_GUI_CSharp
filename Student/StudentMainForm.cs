using Assignment_LibraryManagementSystem_CB010801.Student;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_LibraryManagementSystem_CB010801
{
    public partial class StudentMainForm : Form
    {
        public StudentMainForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AboutUs about = new AboutUs();
            about.Show();
            //this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StudentDashboard dash = new StudentDashboard();
            dash.Show();
            //this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TipsForm tips = new TipsForm();
            tips.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AvailableBooks availableBooks = new AvailableBooks();
            availableBooks.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.Show();
            this.Hide();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            BorrowBooksForm borrowBooks = new BorrowBooksForm();
            borrowBooks.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReturnBooksForm returnBooks = new ReturnBooksForm();
            returnBooks.Show();
            this.Hide();
        }
    }
}
