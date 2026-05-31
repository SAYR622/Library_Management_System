using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;

namespace LMS
{
    public partial class Copy_of_add_member_info : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=yr222;Integrated Security=True");


        string pwd = class1.GetRandomPassword(20);
        string wanted_path;


        public Copy_of_add_member_info()
        {
            InitializeComponent();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
                wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                openFileDialog1.Filter = "Image Files|*.jpeg;*.png;*.jpg;*.gif|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                DialogResult result = openFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {
                    pictureBox1.ImageLocation = openFileDialog1.FileName;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text))
                {
                    MessageBox.Show("Please fill all the textboxes before click register button.");
                    return;
                }

                if (string.IsNullOrEmpty(openFileDialog1.FileName))
                {
                    MessageBox.Show("Please add a picture before click register button.");
                    return;
                }

                if (textBox2.Text != "student" || textBox2.Text != "professor" || textBox2.Text != "lecturer" || textBox2.Text != "admin")
                {
                    MessageBox.Show("Member type could only be a student, professor, lecturer or admin ");
                }
                else
                {
                    string img_path;
                    File.Copy(openFileDialog1.FileName, wanted_path + "\\member_images\\" + pwd + ".jpg");
                    img_path = "member_images\\" + pwd + ".jpg";

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into members_info (member_fullname, member_type, member_email, member_contact, member_username, member_password, member_image) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + img_path.ToString() + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";

                    pictureBox1.Image = null;

                    this.Hide();

                    MessageBox.Show("Registered Successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void LMS_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
