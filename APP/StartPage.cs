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
    public partial class StartPage : Form
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        //timer running one point to a another(progress bar)
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpg_timingPanel.Width += 6;
            if(startpg_timingPanel.Width >= 881)    //when the width 881 reaches stop and direct to the user page
            {
                timer1.Stop();

                User user = new User();
                user.Show();
                this.Hide();
            }
        }

        //Exit program
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StartPage_Load(object sender, EventArgs e)
        {

        }
    }
}
