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
using System.Runtime.InteropServices;

namespace LMS
{
    public partial class login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=yr222;Integrated Security=True");
        int count = 0;
        string membership = "" ;

        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern void SendMessage(
            IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;


        public login()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = true;
            this.DoubleBuffered = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=yr222;Integrated Security=True"))
            {
                con.Open();
              
            }  

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from members_info where member_username='"+ textBox1.Text +"' and member_password ='"+ textBox2.Text +"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            count = dt.Rows.Count;
            con.Close();

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                // Display a message box if either field is empty
                MessageBox.Show("Username or password doesn't match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
            if (count > 0)
             {
                membership = dt.Rows[0]["member_type"].ToString();

                UserCredentials.SetCredentials(textBox1.Text, textBox2.Text);

                if (membership == "admin")
                {                    
                    this.Hide(); // Hide the current form
                    admin_control_form vb = new admin_control_form();
                    vb.ShowDialog(); // Show the new form as a modal dialog
                    Application.Exit();


                }
                else if (membership == "student" || membership == "professor" || membership == "lecturer")
                {
                    this.Hide();
                    others_control_form vb = new others_control_form();
                    vb.ShowDialog(); // Show the new form as a modal dialog
                    Application.Exit();   
                   
                   
                }
            }
            else
            {
                MessageBox.Show("Username or Password does not match");
            }
          }
            }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        public FormClosedEventHandler others_control_form_FormClosed { get; set; }

        private void login_Load(object sender, EventArgs e)
        {
            textBox2.KeyDown += new KeyEventHandler(textBox2_KeyDown);
            textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown);
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        { 
            
            Copy_of_add_member_info vb = new Copy_of_add_member_info();
            vb.Show();
            this.Enabled = false;
            vb.FormClosed += Copy_of_add_member_info_FormClosed;
          
        }

        private void Copy_of_add_member_info_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {


        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel31.Visible = true;
            panel31.BringToFront();
            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel31.Visible = false;
            pictureBox3.BringToFront();
            panel1.BringToFront();
            button2.Visible = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void rectangleShape2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                {                  
                        button1_Click_1(sender, e); // Call the button click event
                        e.SuppressKeyPress = true; // Prevent the 'ding' sound on pressing Enter
                        e.Handled = true;                    
                }
             
            

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                textBox2.Focus(); // Move focus to the password field
                e.SuppressKeyPress = true; // Prevent default behavior (beeping sound)
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void panelTopBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void panelTopBar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

       
      }
  }

