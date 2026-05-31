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
using System.IO;
using System.Collections.ObjectModel;

namespace LMS
{
    public partial class others_control_form : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=yr222;Integrated Security=True");
        int count1 = 0;

        string pwd = "";
        string wanted_path;
        private string selectedImagePath = "";
        private DialogResult result;
        private bool isFirstButtonClick = true;

        //search and reserve books----------------------------------------------------
        int count = 0;
        int num1 = 0;

        string currentUsername = "";
        string currentPassword = "";

        //borrowed and reserved book--------------------------------------------------
        int enroll = 0;




        public others_control_form()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void fill_grid()
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Refresh();

            string storedUsername = UserCredentials.Username;
            string storedPassword = UserCredentials.Password;

            con.Open();


            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from members_info where member_username='" + storedUsername + "' and member_password ='" + storedPassword + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                label35.Text = dr["member_fullname"].ToString();              
            }

            dataGridView2.DataSource = dt;

            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            imageCol.HeaderText = "Member Image";
            imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imageCol.Width = 100;


            dataGridView2.Columns.Insert(8, imageCol);


            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row != null)
                {
                    // Check for Cells["member_image"] existence
                    var memberImageCell = row.Cells["member_image"];
                    if (memberImageCell != null && memberImageCell.Value != null)
                    {
                        imagePath = @"..\..\" + memberImageCell.Value.ToString();

                        if (File.Exists(imagePath))
                        {

                            Image img = Image.FromFile(imagePath);
                            row.Cells[8].Value = img;
                            row.Height = 100;
                        }
                        else
                        {

                        }
                    }
                }

            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        private void SelectImage()
        {

            // Check if any row is selected in the DataGridView
            if (textBox1.Text == "" || Convert.ToInt32(textBox3.Text) == 0 || textBox4.Text == "" || textBox5.Text == "" || textBox2.Text == "" || username.Text == "" || password.Text == "")
            {
                MessageBox.Show("Please select a row first.");
                return;
            }

            wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            openFileDialog1.Filter = "Image Files|*.jpeg;*.png;*.jpg;*.gif|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            result = openFileDialog1.ShowDialog();


            if (result == DialogResult.OK)
            {
                selectedImagePath = openFileDialog1.FileName;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {

            con.Open();

            try
            {
                pwd = class1.GetRandomPassword(20);

                string img_path = "";
                string fileName = pwd + ".jpg";
                string destinationPath = wanted_path + "\\member_images\\" + fileName;

               
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }

                    File.Copy(selectedImagePath, destinationPath);
                    img_path = "member_images\\" + fileName;
                }

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    cmd.CommandText = "update members_info set member_username='" + username.Text + "' ,member_password='" + password.Text + "' ,member_type='" + textBox2.Text + "' ,member_fullname='" + textBox1.Text + "' ,member_image='" + img_path.ToString() + "'  ,member_contact='" + textBox4.Text + "' ,member_email='" + textBox5.Text + "' where member_enrollment_no='" + textBox3.Text + "' ";
                }
                else
                {
                    cmd.CommandText = "update members_info set member_username='" + username.Text + "' ,member_password='" + password.Text + "' ,member_type='" + textBox2.Text + "' ,member_fullname='" + textBox1.Text + "'  ,member_contact='" + textBox4.Text + "' ,member_email='" + textBox5.Text + "' where member_enrollment_no='" + textBox3.Text + "' ";
                }

                cmd.ExecuteNonQuery();


                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                     if (File.Exists(selectedImagePath))
                        {
                            selectedImagePath = "";
                        }
                }

                fill_grid();
                fill_picturebox();
               
                MessageBox.Show("Details Updated Successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {      
                SelectImage();                         
        }       
            
        

        public DataTable GetMemberInfoDataTable()
        {
            // Return the DataTable dt
            return dt;
        }

        public string imagePath { get; set; }


        public DataTable dt { get; set; }





        private void update_profile_details_Load(object sender, EventArgs e)
        
            {
            //home-------------------------------
                   fill_picturebox();
      
            
            //update profile details--------------------------------------------------------------------------------------------

                    con.Open();

                    string storedUsername = UserCredentials.Username;
                    string storedPassword = UserCredentials.Password;

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from members_info where member_username='" + storedUsername + "' and member_password ='" + storedPassword + "'";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    count1 = dt.Rows.Count;

                    con.Close();

                    if (count1 > 0)
                    {

                        foreach (DataRow dr in dt.Rows)
                        {

                            textBox1.Text = dt.Rows[0]["member_fullname"].ToString();
                            textBox2.Text = dt.Rows[0]["member_type"].ToString();
                            textBox3.Text = dt.Rows[0]["member_enrollment_no"].ToString();
                            textBox4.Text = dt.Rows[0]["member_contact"].ToString();
                            textBox5.Text = dt.Rows[0]["member_email"].ToString();
                            username.Text = dt.Rows[0]["member_username"].ToString();
                            password.Text = dt.Rows[0]["member_password"].ToString();

                            panel1.Visible = true;
                            //panel1.Location = new Point((Screen.PrimaryScreen.Bounds.Width - panel1.Width) / 2,
                            //             (Screen.PrimaryScreen.Bounds.Height - panel1.Height) / 2);
                            //this.WindowState = FormWindowState.Maximized;

                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

          
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username or Password does not match");
                    }

                    GetMemberInfoDataTable();
                    fill_grid();

                    if (!isFirstButtonClick)
                    {
                        button1.Enabled = true;
                    }

            //search and reserve books--------------------------------------------------------------------------------------
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    fill_book_list();

            //borrowed and reserved books-----------------------------------------------------------------------------------
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    fill_borrowed_books_and_deadlines();
                    dataGridView4.CellFormatting += dataGridView4_CellFormatting;
                
            }

        private void fill_picturebox()
        {
            string storedUsername = UserCredentials.Username;
            string storedPassword = UserCredentials.Password;

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT member_image FROM members_info WHERE member_username = @username AND member_password = @password";
            cmd.Parameters.AddWithValue("@username", storedUsername);
            cmd.Parameters.AddWithValue("@password", storedPassword);

            string relativeImagePath = cmd.ExecuteScalar() as string;
            con.Close();

            if (!string.IsNullOrEmpty(relativeImagePath))
            {
                string fullPath = AppDomain.CurrentDomain.BaseDirectory;
                fullPath = fullPath.Substring(0, fullPath.IndexOf("bin")) + relativeImagePath;


                Console.WriteLine("Full Path: " + fullPath); // Debugging: Output the path to the console


                if (File.Exists(fullPath))
                {
                    try
                    {
                        pictureBox1.Image = Image.FromFile(fullPath);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading image: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found at: " + fullPath);
                }
            }
            else
            {
                
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            panel2.BringToFront();
            panel2.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel3.BringToFront();
            panel2.Visible = false;
            panel4.Visible = false;
            button5.Visible = false;
        }

        //search and reserve books-----------------------------------------------------------------------------------------
        public void fill_book_list()
        {
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select Id,book_name,book_author_name,book_publication_name,available_qty from books_info where available_qty > 0";
            cmd1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int id = 0;

            string storedUsername = UserCredentials.Username;
            string storedPassword = UserCredentials.Password;

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            if (int.TryParse(member_enroll.Text, out num1))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT member_enrollment_no, member_username, member_password FROM members_info WHERE member_enrollment_no ='" + Convert.ToInt32(member_enroll.Text) + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                count = dt.Rows.Count;

                if (count == 0)
                {
                    MessageBox.Show("Your enrollment no or book name is incorrect. Try Again");
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        currentUsername = dr["member_username"].ToString();
                        currentPassword = dr["member_password"].ToString();
                        
                            {
                                SqlCommand cmd1 = con.CreateCommand();
                                cmd1.CommandType = CommandType.Text;
                                cmd1.CommandText = "SELECT book_name, id FROM books_info WHERE book_name ='" + book_name.Text + "' ";
                                cmd1.ExecuteNonQuery();
                                DataTable dt1 = new DataTable();
                                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                da1.Fill(dt1);

                                count = dt1.Rows.Count;

                                if (count == 0)
                                {
                                    MessageBox.Show("Book name is incorrect. Try again or select the book from the table");
                                }
                                else
                                {
                                    SqlCommand countCmd = con.CreateCommand();
                                    countCmd.CommandType = CommandType.Text;
                                    countCmd.CommandText = "SELECT COUNT(*) FROM book_reservations WHERE member_enrollment_no = @memberEnrollment";
                                    countCmd.Parameters.AddWithValue("@memberEnrollment", member_enroll.Text);

                                    int existingRowCount = (int)countCmd.ExecuteScalar();

                                    if (existingRowCount >= 2)
                                    {
                                        MessageBox.Show("You already have two reservations. Cannot make more reservations.");
                                    }
                                    else
                                    {
                                        SqlCommand cmd4 = con.CreateCommand();
                                        cmd4.CommandType = CommandType.Text;
                                        cmd4.CommandText = "SELECT member_fullname, member_type FROM members_info WHERE member_enrollment_no ='" + member_enroll.Text + "'";
                                        cmd4.ExecuteNonQuery();
                                        DataTable dt4 = new DataTable();
                                        SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                                        da4.Fill(dt4);

                                        if (dt4.Rows.Count > 0)
                                        {
                                            DataRow dr4 = dt4.Rows[0]; // Assuming only one row is fetched

                                            // Fetch member details
                                            string memberFullName = dr4["member_fullname"].ToString();
                                            string memberType = dr4["member_type"].ToString();
                                            // Combined insert operation for book_reservations
                                            SqlCommand cmd3 = con.CreateCommand();
                                            cmd3.CommandType = CommandType.Text;
                                            cmd3.CommandText = "INSERT INTO book_reservations (reserved_book, member_enrollment_no, reserved_date, fetching_date, member_name, member_type) " +
                                                "VALUES (@bookName, @memberEnrollment, @reservedDate, @fetchingDate, @memberFullName, @memberType)";

                                            // Add parameters for insertion
                                            cmd3.Parameters.AddWithValue("@bookName", book_name.Text);
                                            cmd3.Parameters.AddWithValue("@memberEnrollment", member_enroll.Text);
                                            cmd3.Parameters.AddWithValue("@reservedDate", dateTimePicker1.Value.ToLongDateString());
                                            cmd3.Parameters.AddWithValue("@fetchingDate", dateTimePicker2.Value.ToLongDateString());
                                            cmd3.Parameters.AddWithValue("@memberFullName", memberFullName);
                                            cmd3.Parameters.AddWithValue("@memberType", memberType);

                                            cmd3.ExecuteNonQuery(); // Insertion

                                            // Query to fetch the reservation ID after insertion
                                            SqlCommand cmd5 = con.CreateCommand();
                                            cmd5.CommandType = CommandType.Text;
                                            cmd5.CommandText = "SELECT ID FROM book_reservations " +
                                                "WHERE reserved_book = @bookName " +
                                                "AND member_enrollment_no = @memberEnrollment " +
                                                "AND reserved_date = @reservedDate " +
                                                "AND fetching_date = @fetchingDate " +
                                                "AND member_name = @memberFullName " +
                                                "AND member_type = @memberType";

                                            // Add parameters for fetching
                                            cmd5.Parameters.AddWithValue("@bookName", book_name.Text);
                                            cmd5.Parameters.AddWithValue("@memberEnrollment", member_enroll.Text);
                                            cmd5.Parameters.AddWithValue("@reservedDate", dateTimePicker1.Value.ToLongDateString());
                                            cmd5.Parameters.AddWithValue("@fetchingDate", dateTimePicker2.Value.ToLongDateString());
                                            cmd5.Parameters.AddWithValue("@memberFullName", memberFullName);
                                            cmd5.Parameters.AddWithValue("@memberType", memberType);

                                            id = (int)cmd5.ExecuteScalar(); // Fetching the ID

                                            if (id > 0)
                                            {
                                                MessageBox.Show("Your reservation has been collected. We will accept it soon. Your reservation ID is " + id);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Failed to retrieve reservation ID.");
                                            }
                                        }
                                    }
                                }
                            }
                                              
                    }
                }
            }
            else
            {
                MessageBox.Show("Your enrollment no is not a number. Try again");
            }
            con.Close();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int i;
            if (dataGridView1.SelectedCells[0].Value != null && dataGridView1.SelectedCells[0].Value.ToString() != "")
            {
                if (int.TryParse(dataGridView1.SelectedCells[0].Value.ToString(), out i))
                {
                    i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

                    try
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }


                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select book_name from books_info where id=" + i + "";
                        cmd.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        con.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                            book_name.Text = dr["book_name"].ToString();
                        }

                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("No value selected or empty cell.");
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            int x = 0;
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Id,book_name,book_author_name,book_publication_name,available_qty from books_info where book_name like('%" + textBox7.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                x = Convert.ToInt32(dt.Rows.Count.ToString());

                dataGridView1.DataSource = dt;
                con.Close();

                if (x == 0)
                {
                    MessageBox.Show("No books were found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            int x = 0;
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Id,book_name,book_author_name,book_publication_name,available_qty from books_info where book_name like('%" + textBox7.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                x = Convert.ToInt32(dt.Rows.Count.ToString());

                dataGridView1.DataSource = dt;
                con.Close();

                if (x == 0)
                {
                    label13.Visible = true;
                    dataGridView1.Visible = false;
                }
                else
                {
                    label13.Visible = false;
                    dataGridView1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Id,book_name,book_author_name,book_publication_name,available_qty from books_info where book_name like('%" + textBox7.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Id,book_name,book_author_name,book_publication_name,available_qty from books_info where book_category like('%" + textBox6.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());

                dataGridView1.DataSource = dt;
                con.Close();

                if (i == 0)
                {
                    MessageBox.Show("No books were found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Id,book_name,book_author_name,book_publication_name,available_qty from books_info where book_category like('%" + textBox6.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());

                dataGridView1.DataSource = dt;
                con.Close();

                if (i == 0)
                {
                    label13.Visible = true;
                    dataGridView1.Visible = false;
                }
                else
                {
                    label13.Visible = false;
                    dataGridView1.Visible = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            {
                try
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select Id,book_name,book_author_name,book_publication_name,available_qty from books_info where book_category like('%" + textBox6.Text + "%')";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            member_enroll.Text = textBox3.Text;
            panel4.Visible = true;
            button5.Visible = true;
            panel4.BringToFront();
        }


  //borrowed and reserved books-------------------------------------------------------------------------------------------------------------------------------------------------------

        private void dataGridView4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView4.Columns[e.ColumnIndex].Name == "deadline_date")
            {
                dataGridView4.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                dataGridView4.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
            }
        }


        public void fill_borrowed_books_and_deadlines()
        {
            label23.Visible = false;
            label25.Visible = false;

            string storedUsername = UserCredentials.Username;
            string storedPassword = UserCredentials.Password;

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from members_info where member_username='" + storedUsername + "' and member_password ='" + storedPassword + "'";
            con.Close();

            try
            {


                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    enroll = Convert.ToInt32(dt.Rows[0]["member_enrollment_no"]);

                    con.Open();
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "select member_enrollment_no, member_name, reserved_book, reserved_date, fetching_date, pending_or_accepted from book_reservations where member_enrollment_no = @enroll and pending_or_accepted = '" + "Pending" + "'";
                    cmd1.Parameters.AddWithValue("@enroll", enroll);

                    DataTable dt1 = new DataTable();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    da1.Fill(dt1);
                    con.Close();

                    if (dt1.Rows.Count > 0)
                    {
                        dataGridView5.DataSource = dt1;
                        dataGridView5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    else
                    {
                        label25.Visible = true;
                    }






                    con.Open();
                    SqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "select member_enrollment, member_name, book_name, book_issued_date, deadline_date from issue_books where member_enrollment = @enroll";
                    cmd2.Parameters.AddWithValue("@enroll", enroll);

                    DataTable dt2 = new DataTable();
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    da2.Fill(dt2);
                    con.Close();

                    if (dt2.Rows.Count > 0)
                    {
                        dataGridView4.DataSource = dt2;
                        dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    else
                    {
                        label26.Visible = true;
                    }




                    con.Open();
                    SqlCommand cmd3 = con.CreateCommand();
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = "select member_enrollment_no, member_name, reserved_book, reserved_date, fetching_date, pending_or_accepted from book_reservations where member_enrollment_no = @enroll and pending_or_accepted = '" + "Accepted" + "'";
                    cmd3.Parameters.AddWithValue("@enroll", enroll);

                    DataTable dt3 = new DataTable();
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    da3.Fill(dt3);
                    con.Close();

                    if (dt3.Rows.Count > 0)
                    {
                        dataGridView3.DataSource = dt3;
                        dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    else
                    {
                        label23.Visible = true;
                    }




                }
                else
                {
                    MessageBox.Show("No member information found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void textBox8_KeyUp(object sender, KeyEventArgs e)
        {
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select member_enrollment, member_name, book_name, book_issued_date, deadline_date from issue_books where member_enrollment = @enroll and book_name like ('%" + textBox8.Text + "%')";
            cmd1.Parameters.AddWithValue("@enroll", enroll);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(dt);
            dataGridView4.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            button5.Visible = true;
            panel6.BringToFront();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            panel31.BringToFront();
            panel31.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
            login vb = new login();
            vb.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            fill_borrowed_books_and_deadlines();
        }


        }
    }

