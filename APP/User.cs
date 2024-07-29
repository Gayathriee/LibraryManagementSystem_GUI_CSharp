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
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }


        //directing to the lib login
        private void button2_Click(object sender, EventArgs e)
        {
            LibrarianLoginForm login = new LibrarianLoginForm();
            login.Show();
            this.Hide();
        }


        //directing to student login
        private void button1_Click(object sender, EventArgs e)
        {
            StudentLogin Stulogin = new StudentLogin();
            Stulogin.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
