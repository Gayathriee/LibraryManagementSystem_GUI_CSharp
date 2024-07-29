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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        //exit program
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //directing to the student form

        private void button1_Click(object sender, EventArgs e)
        {
            StudentForm student = new StudentForm();
            student.Show();
            this.Hide();
        }


        //directing to the book form
        private void button2_Click(object sender, EventArgs e)
        {
            BookTbl book = new BookTbl();
            book.Show();
            this.Hide();
        }


        //directing to the lib form
        private void button5_Click(object sender, EventArgs e)
        {
            LibrarianForm lib = new LibrarianForm();
            lib.Show();
            this.Hide();
        }


        //directing to the issue book page
        private void button3_Click(object sender, EventArgs e)
        {
            issueBooks issue = new issueBooks();
            issue.Show();
            this.Hide();
        }


        //directing to the return form page

        private void button4_Click(object sender, EventArgs e)
        {
            ReturnForm returnF = new ReturnForm();
            returnF.Show();
            this.Hide();
        }


        //directing to about us page
        private void button6_Click(object sender, EventArgs e)
        {
            AboutUs about = new AboutUs();
            about.Show();
            //this.Hide();
        }


        //directing to the dashboard
        private void button7_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
            dash.Show();
            //this.Hide();
        }


        //directing to the tips page
        private void button8_Click(object sender, EventArgs e)
        {
            TipsForm tips = new TipsForm();
            tips.Show();

        }


        //directing to the user page
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.Show();
            this.Hide();

        }
    }
}
