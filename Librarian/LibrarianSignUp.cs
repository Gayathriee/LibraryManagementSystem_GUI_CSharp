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
    public partial class LibrarianSignUp : Form
    {
        public LibrarianSignUp()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=Gayathrie\SQLEXPRESS;Initial Catalog=users;Integrated Security=True;Encrypt=False");
        //SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g3hap\OneDrive\Documents\MyLibraryDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void signln_btn_Click(object sender, EventArgs e)
        {
            LibrarianLoginForm loginf = new LibrarianLoginForm();
            loginf.Show();
            this.Hide();
        }

        private void register_showPass_CheckedChanged(object sender, EventArgs e)
        {
            Libregister_password.PasswordChar = register_showPass.Checked ? '\0' : '*';
        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            if (Libregister_ID.Text == "" || Libregister_username.Text == "" || Libregister_password.Text == "")
            {
                MessageBox.Show("Please fill all the blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Con.State != ConnectionState.Open)
                {
                    try
                    {
                        Con.Open();

                        String checkUsername = "SELECT COUNT(*) FROM LibrarianTbl WHERE LibName = @LibName";

                        using (SqlCommand checkCMD = new SqlCommand(checkUsername, Con))
                        {
                            checkCMD.Parameters.AddWithValue("@LibName", Libregister_username.Text.Trim());
                            int count = (int)checkCMD.ExecuteScalar();

                            if (count > 1)
                            {
                                MessageBox.Show(Libregister_username.Text.Trim()
                                    + "is already taken", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                String insertData = "INSERT INTO LibrarianTbl (LibID, LibName, LibPassword, LibPhone) " +
 " Values (@LibID,@LibName,@LibPassword, @LibPhoneDefault)";

                                using (SqlCommand insertCMD = new SqlCommand(insertData, Con))
                                {
                                    insertCMD.Parameters.AddWithValue("@LibID", Libregister_ID.Text.Trim());
                                    insertCMD.Parameters.AddWithValue("@LibName", Libregister_username.Text.Trim());
                                    insertCMD.Parameters.AddWithValue("@LibPassword", Libregister_password.Text.Trim());
                                    insertCMD.Parameters.AddWithValue("@LibPhoneDefault", 0); // Set default value of LibPhone to 0

                                    insertCMD.ExecuteNonQuery();

                                    MessageBox.Show("Register successfully!", "Information Message"
                                        , MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    LibrarianLoginForm lForm = new LibrarianLoginForm();
                                    lForm.Show();
                                    this.Hide();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error connecting Database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Con.Close();
                    }
                }
            }
        }
    }
    }

