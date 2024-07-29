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
    public partial class AboutUs : Form
    {
        public AboutUs()
        {
            InitializeComponent();
        }


        //Exit button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
           
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }
    }
}
