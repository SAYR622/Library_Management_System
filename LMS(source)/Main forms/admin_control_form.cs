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
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections.ObjectModel;

namespace LMS
{
   public partial class admin_control_form : Form
    {
       SqlConnection con = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=yr222;Integrated Security=True");

        private int childFormNumber = 0;
        bool sidebarExpand = false;
        bool addCollapse = true;
        bool vudCollapse = true;
        bool issuedCollapse = true;
        int maxHeight = 330;
        int maxHeight1 = 224;
        int miniHeight = 49;
        int x;
       //return books
        string q = "a";

       //reserved books ----------------------------------------
        string acce = "Accepted";

       //add books --------------------------------------------
        SqlDataAdapter da;
        DataTable dt;

       //add members ------------------------------------------
        string pwd = "";
        string wanted_path;
       //vud members--------------------------------------------
        string pwd1 = "";
        private string selectedImagePath = "";
        private DialogResult result;
        private bool isFirstButtonClick = true;
        



        public admin_control_form()
        {
            InitializeComponent();
            vudContainer.Resize += vudContainer_Resize;
            //return books-------------------------------------------------------------
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

           //reserved books------------------------------------------------------------
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //add books ---------------------------------------------------------------
            DisplayData();

            //issued books ------------------------------------------------------------
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView5.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView6.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView7.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //every panel--------------------------------------------------------------
            panel1.Click += Panel_Click;
            panel2.Click += Panel_Click;
            panel3.Click += Panel_Click;
            panel4.Click += Panel_Click;
            panel5.Click += Panel_Click;
            panel6.Click += Panel_Click;
            panel7.Click += Panel_Click;
            panel8.Click += Panel_Click;
            panel9.Click += Panel_Click;
            panel10.Click += Panel_Click;
            panel11.Click += Panel_Click;
            panel12.Click += Panel_Click;
            panel13.Click += Panel_Click;
            panel14.Click += Panel_Click;
            panel15.Click += Panel_Click;
            panel16.Click += Panel_Click;
            panel17.Click += Panel_Click;
            panel18.Click += Panel_Click;
            panel19.Click += Panel_Click;
            panel20.Click += Panel_Click;
            panel21.Click += Panel_Click;
            panel22.Click += Panel_Click;
            panel23.Click += Panel_Click;
            panel24.Click += Panel_Click;
            panel25.Click += Panel_Click;
            panel26.Click += Panel_Click;
            panel27.Click += Panel_Click;
            panel28.Click += Panel_Click;
            panel29.Click += Panel_Click;
            panel30.Click += Panel_Click;
            panel31.Click += Panel_Click;

        }




        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void addNewBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void viewAndUpdateBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void issueBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void bookRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void returnBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void addAMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void viewAndUpdateMemberInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void bookReservationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {

                    if (issuedContainer.Height == issuedContainer.MaximumSize.Height)
                    {
                        issuedContainer.Height -= 106;
                        issuedCollapse = true;
                        issuedTimer.Stop();
                    }


                    if (vudContainer.Height == vudContainer.MaximumSize.Height)
                    {

                        vudContainer.Height -= 281;
                        vudCollapse = true;
                        vudTimer.Stop();
                        vudContainer.MaximumSize = new System.Drawing.Size(vudContainer.Width, maxHeight1);

                    }

                    if (addContainer.Height == addContainer.MaximumSize.Height)
                    {
                        addContainer.Height -= 120;
                        addCollapse = true;
                        addTimer.Stop();
                    }
                        button4.Enabled = false;
                        button5.Enabled = false;    
                        sidebarTimer.Stop();
                        sidebarExpand = false;
                        
                        
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                        button4.Enabled = true;
                        button5.Enabled = true;
                        sidebarTimer.Stop();
                        sidebarExpand = true;
                        
                        
                }
            }
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
           
        }

        private void sidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addTimer_Tick(object sender, EventArgs e)
        {
            if (addCollapse)
            {
                addContainer.Height += 10;
                if (addContainer.Height == addContainer.MaximumSize.Height)
                {
                    addCollapse = false;
                    addTimer.Stop();

                }
            }
            else
            {
                addContainer.Height -= 10;
                if (addContainer.Height == addContainer.MinimumSize.Height)
                {
                    addCollapse = true;
                    addTimer.Stop();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    addTimer.Start();
                }
            }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void vudTimer_Tick(object sender, EventArgs e)
        {
            if (vudCollapse)
            {
                vudContainer.Height += 10;
                if (vudContainer.Height == vudContainer.MaximumSize.Height)
                {
                    vudCollapse = false;
                    vudTimer.Stop();

                }
            }

            else
            {
                
                vudContainer.Height -= 10;
                if (vudContainer.Height == vudContainer.MinimumSize.Height)
                {
                     vudCollapse = true;
                     vudTimer.Stop();
                }

            }

         }
        

        private void button5_Click(object sender, EventArgs e)
        {
            if (sidebar.Width == sidebar.MaximumSize.Width)
            {
                if (vudContainer.Height == miniHeight)
                {
                    vudContainer.MinimumSize = new System.Drawing.Size(vudContainer.Width, miniHeight);
                    vudTimer.Start();
                }
                if (issuedContainer.Size == issuedContainer.MaximumSize && vudContainer.Height == maxHeight)
                {
                    vudContainer.MaximumSize = new System.Drawing.Size(vudContainer.Width, maxHeight1);
                    issuedCollapse = false;
                    issuedTimer.Start();
                    vudTimer.Start();
                }
                if (vudContainer.Height == maxHeight1)
                {
                    vudContainer.MaximumSize = new System.Drawing.Size(vudContainer.Width, maxHeight1);
                    vudTimer.Start();
                }

            }
        }

       //issue books---------------------------------------------------------------------------------------------------------

        private void button15_Click(object sender, EventArgs e)
        {
            int i = 0;

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from members_info where member_enrollment_no='" + txt_enrollment.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
            con.Close();

            if (i == 0)
            {
                MessageBox.Show("This enrollment not found");
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txt_studentname.Text = dr["member_fullname"].ToString();
                    txt_studentcontact.Text = dr["member_contact"].ToString();
                    txt_studentemail.Text = dr["member_email"].ToString();
                    txt_membership.Text = dr["member_type"].ToString();

                }
            }
        }

        private void admin_control_form_Load(object sender, EventArgs e)
        {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

            //reserved books -------------------------------------------------
                fill_reserved_books();
            //issued books ---------------------------------------------------
                fill_books_info_book_name();
                fill_books_info_member_name();
            //vud members------------------------------------------------------
                con.Open();
                fill_grid_members();
            //vud books----------------------------------------------------------
                con.Close();
                disp_books();

            
        }

        private void txt_bookname_KeyUp(object sender, KeyEventArgs e)
        {
            int count = 0;
            if (e.KeyCode != Keys.Enter)
            {

                listBox1.Items.Clear();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where book_name like('%" + txt_bookname.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());
                con.Close();

                if (count > 0)
                {

                    listBox1.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        listBox1.Items.Add(dr["book_name"].ToString());
                    }
                }
            }
        }

        private void txt_bookname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_bookname.Text = listBox1.SelectedItem.ToString();
                listBox1.Visible = false;
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            txt_bookname.Text = listBox1.SelectedItem.ToString();
            listBox1.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_studentname.Text) ||
                string.IsNullOrWhiteSpace(txt_studentcontact.Text) ||
                string.IsNullOrWhiteSpace(txt_studentemail.Text) ||
                string.IsNullOrWhiteSpace(txt_bookname.Text))
            {
                MessageBox.Show("Please fill all the fields.");
            }
            else
            {
                con.Open();
                int books_qty = 0;
                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select * from books_info where book_name = '" + txt_bookname.Text + "'";
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);               

                foreach (DataRow dr2 in dt2.Rows)
                {
                    books_qty = Convert.ToInt32(dr2["available_qty"].ToString());
                }

                if (books_qty > 0)
                {


                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into issue_books (membership,member_enrollment, member_name, member_contact, member_email, book_name, book_issued_date,deadline_date) values('" + txt_membership.Text + "','" + txt_enrollment.Text + "','" + txt_studentname.Text + "','" + txt_studentcontact.Text + "','" + txt_studentemail.Text + "','" + txt_bookname.Text + "','" + dateTimePicker1.Value.ToLongDateString() + "','" + dateTimePicker2.Value.ToLongDateString() + "')";
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "update books_info set available_qty = available_qty-1 where book_name='" + txt_bookname.Text + "'";
                    cmd1.ExecuteNonQuery();

                    SqlCommand cmd3 = con.CreateCommand();
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = "delete from book_reservations where pending_or_accepted = '" + "Accepted" + "' and member_enrollment_no = '" + txt_enrollment.Text + "' and reserved_book = '" + txt_bookname.Text + "' ";
                    cmd3.ExecuteNonQuery();

                    MessageBox.Show("Book issued successfully");

                }
                else
                {
                    MessageBox.Show("Book is not available");
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                panel9.Visible = true;
                panel21.Visible = false;
                panel10.Visible = false;
                panel20.Visible = false;
                panel26.Visible = false;
                panel24.Visible = false;
                panel22.Visible = false;
                panel27.Visible = false;
                panel29.Visible = false;
                panel31.Visible = false;
                
                 

        }

       //return books ---------------------------------------------------------------------------------------------------------------------

        private void button17_Click(object sender, EventArgs e)
        {
            label81.Visible = false;
           
            fill_grid(textBox1.Text);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        public void fill_grid(string enrollment)
        {           
            
             string storedUsername = UserCredentials.Username;
             string storedPassword = UserCredentials.Password;

             if (con.State == ConnectionState.Closed)
             {
                 con.Open();
             }         

            
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select id,member_enrollment, membership, member_name, member_contact, member_email, book_name, book_issued_date, deadline_date from issue_books where member_enrollment='" + enrollment.ToString() + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            
            string q = textBox1.Text;

            

            if (dt.Rows.Count == 0)
            {
                if (q == "")
                {
                    label81.Visible = true;
                }
                else
                {
                    dataGridView1.DataSource = null;
                    label82.Visible = true;
                }
            }
            else
            {
                dataGridView1.DataSource = dt;
                label82.Visible = false;
                label81.Visible = false;
            }
                 

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            con.Close();
            panel17.Visible = true;
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from issue_books where id= " + i + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                lbl_bookname.Text = dr["book_name"].ToString();
                lbl_issueddate.Text = Convert.ToString(dr["book_issued_date"].ToString());
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //try
           // {
                con.Open();
                int i;
                i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

                DateTime deadlineDate;

                
                SqlCommand deadlineCmd = con.CreateCommand();
                deadlineCmd.CommandType = CommandType.Text;
                deadlineCmd.CommandText = "select deadline_date from issue_books where id= " + i + "";


                using (SqlDataReader deadlineReader = deadlineCmd.ExecuteReader())
                {
                    if (deadlineReader.Read())
                    {
                        deadlineDate = Convert.ToDateTime(deadlineReader["deadline_date"]);
                    }
                    else
                    {
                        MessageBox.Show("Deadline date not found.");
                        return;
                    }

                    deadlineReader.Close();
                    
                }

                DateTime returnDate = dateTimePicker1.Value;
                TimeSpan difference = returnDate - deadlineDate;
                int daysDifference = (int)difference.TotalDays;

                int fine = 0;

                if (daysDifference > 0)
                {
                    fine = daysDifference * 10;

                    MessageBox.Show("Book returned successfully. Fine: Rs." + fine);

                    
                    SqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "delete from issue_books where id= " + i + "";
                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "update books_info set available_qty= available_qty+1 where book_name='" + lbl_bookname.Text + "'";
                    cmd1.ExecuteNonQuery();
                    
                }
                else
                {
                    MessageBox.Show("Book returned successfully");

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    
                    SqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "delete from issue_books where id= " + i + "";
                    con.Open();
                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "update books_info set available_qty= available_qty+1 where book_name='" + lbl_bookname.Text + "'";
                    cmd1.ExecuteNonQuery();
                    
                }

                panel17.Visible = true;

                fill_grid(textBox1.Text);
                con.Close();
           // }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fill_grid(textBox1.Text);
                panel10.Visible = true;
                panel21.Visible = false;
                panel9.Visible = false;
                panel20.Visible = false;
                panel26.Visible = false;
                panel24.Visible = false;
                panel22.Visible = false;
                panel27.Visible = false;
                panel29.Visible = false;
                panel31.Visible = false;
                         
        }

       //reserved books --------------------------------------------------------------------------------------------------------------

        public void fill_reserved_books()
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Refresh();

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }


            con.Open();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from book_reservations";
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            con.Close();

            if (dt1.Rows.Count > 0)
            {
                dataGridView2.DataSource = dt1;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                label20.Visible = true;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            {

                int i;
                if (dataGridView2.SelectedCells[0].Value != null && dataGridView2.SelectedCells[0].Value.ToString() != "")
                {
                    if (int.TryParse(dataGridView2.SelectedCells[0].Value.ToString(), out i))
                    {
                        i = Convert.ToInt32(dataGridView2.SelectedCells[0].Value.ToString());

                        try
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }


                            con.Open();
                            SqlCommand cmd = con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select * from book_reservations where id=" + i + "";
                            cmd.ExecuteNonQuery();
                            DataTable dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt);
                            con.Close();
                            foreach (DataRow dr in dt.Rows)
                            {
                                ID.Text = dr["ID"].ToString();
                                name.Text = dr["member_name"].ToString();
                                enroll.Text = dr["member_enrollment_no"].ToString();
                                book.Text = dr["reserved_book"].ToString();
                                fetch.Text = dr["fetching_date"].ToString();
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
        }

        private void dataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from book_reservations where id='" + ID.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                if(acce != dr["pending_or_accepted"].ToString())
                {
            

            
            con.Open();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;

            cmd1.CommandText = "update book_reservations set pending_or_accepted = '" + "Accepted" + "'  where ID='" + ID.Text + "' ";
            cmd1.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;

            cmd2.CommandText = "update books_info set available_qty = available_qty-1  where ID='" + ID.Text + "' ";
            cmd2.ExecuteNonQuery();
            con.Close();

            fill_reserved_books();

            MessageBox.Show("Reservation Accepted");

                }
                else
                {
                    MessageBox.Show ("It is already Accepted");
                }
                fill_reserved_books();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fill_reserved_books();
                panel20.Visible = true; 
                panel21.Visible = false;
                panel9.Visible = false;
                panel10.Visible = false;
                panel26.Visible = false;
                panel24.Visible = false;
                panel22.Visible = false;
                panel29.Visible = false;
                panel31.Visible = false;
                panel27.Visible = false;
                
               
           
        }


     //add books --------------------------------------------------------------------------------------------------------------------

        private void DisplayData()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            da = new SqlDataAdapter("select * from books_info", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            dataGridView3.ReadOnly = true;
            con.Close();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into books_info (book_name, book_category, book_author_name, book_publication_name, book_purchase_date, book_price, book_quantity) values('" + textBox4.Text + "','" + textBox7.Text + "','" + textBox3.Text + "','" + textBox2.Text + "','" + dateTimePicker4.Value.ToLongDateString() + "'," + Convert.ToInt32(textBox5.Text) + "," + Convert.ToInt32(textBox6.Text) + ")";
            cmd.ExecuteNonQuery();
            con.Close();

            textBox4.Text = "";
            textBox7.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

            MessageBox.Show("Book Added Successfully");

            DisplayData();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel21.Visible = false;
            panel29.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DisplayData();
            panel21.Visible = true;   
            panel9.Visible = false;
            panel10.Visible = false;
            panel20.Visible = false;
            panel26.Visible = false;
            panel24.Visible = false;
            panel22.Visible = false;
            panel29.Visible = false;
            panel31.Visible = false;
            panel27.Visible = false;
             
        }


        //add members ----------------------------------------------------------------------------------------------------------------

        private void button20_Click_1(object sender, EventArgs e)
        {
            wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            openFileDialog2.Filter = "Image Files|*.jpeg;*.png;*.jpg;*.gif|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            DialogResult result = openFileDialog2.ShowDialog();

            if (result == DialogResult.OK)
            {
                pictureBox2.ImageLocation = openFileDialog2.FileName;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox13.Text) || string.IsNullOrEmpty(textBox10.Text) || string.IsNullOrEmpty(textBox11.Text) || string.IsNullOrEmpty(textBox12.Text) || string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox9.Text))
                {
                    MessageBox.Show("Please fill all the textboxes before click register button.");
                    return;
                }

                if (string.IsNullOrEmpty(openFileDialog2.FileName))
                {
                    MessageBox.Show("Please add a picture before click register button.");
                    return;
                }

                if (textBox10.Text != "student" && textBox10.Text != "professor" && textBox10.Text != "lecturer")
                {
                    MessageBox.Show("Member type could only be a student, professor or lecturer ");
                }
                else
                {

                    string img_path;

                    pwd = class1.GetRandomPassword(20);
                    File.Copy(openFileDialog2.FileName, wanted_path + "\\member_images\\" + pwd + ".jpg");
                    img_path = "member_images\\" + pwd + ".jpg";

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into members_info (member_fullname, member_type, member_email, member_contact, member_username, member_password, member_image) values('" + textBox13.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + textBox12.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + img_path.ToString() + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    textBox13.Text = "";
                    textBox10.Text = "";
                    textBox11.Text = "";
                    textBox12.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";

                    pictureBox2.Image = null;

                    MessageBox.Show("Registered Successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


      private void button7_Click(object sender, EventArgs e)
        {
            panel21.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel20.Visible = false;
            panel26.Visible = false;
            panel24.Visible = false;
            panel27.Visible = false;
            panel29.Visible = false;
            panel31.Visible = false;
            panel22.Visible = true; 
        }


        //menu button start part-------------------------------------------------
        private void button23_Click(object sender, EventArgs e)
        {
            
            sidebarTimer.Start();

        }      
        //----------------------------------------------------------------------

       
       //issued books search by book name ------------------------------------------------------------------------------------------------------------------
        public void fill_books_info_book_name()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select book_name,book_author_name,book_quantity,available_qty from books_info";
            cmd1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(dt);
            dataGridView5.DataSource = dt;
            dataGridView5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            con.Close();
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            con.Open();
            string i;
            i = dataGridView5.SelectedCells[0].Value.ToString();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from issue_books where book_name='" + i.ToString() + "' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView4.DataSource = dt;
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            con.Close();
        }

        private void textBox15_KeyUp(object sender, KeyEventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select book_name,book_author_name,book_quantity,available_qty from books_info where book_name like ('%" + textBox15.Text + "%')";
            cmd1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(dt);
            dataGridView5.DataSource = dt;
            dataGridView5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            con.Close();
        }


       //-----------------------------------------------------------------------------------------------------------------------------------

        private void button14_Click(object sender, EventArgs e)
        {
            if (sidebar.Width == sidebar.MaximumSize.Width)
            {
                    if (vudContainer.Height == maxHeight1 && issuedContainer.Size == issuedContainer.MinimumSize)
                    {
                        vudContainer.MaximumSize = new System.Drawing.Size(vudContainer.Width, maxHeight);
                        vudCollapse = true;
                        vudTimer.Start();
                        issuedTimer.Start();
                    }
                    if (vudContainer.Height == maxHeight && issuedContainer.Size == issuedContainer.MaximumSize)
                    {
                        vudContainer.MaximumSize = new System.Drawing.Size(vudContainer.Width, maxHeight1);
                        vudCollapse = true;
                        vudTimer.Start();
                        issuedTimer.Start();
                    }
               
            }
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void issuedTimer_Tick(object sender, EventArgs e)
        {
            {
                if (issuedCollapse)
                {
                    issuedContainer.Height += 10;
                    if (issuedContainer.Height == issuedContainer.MaximumSize.Height)
                    {
                        issuedCollapse = false;
                        issuedTimer.Stop();

                    }
                }
                else
                {
                    issuedContainer.Height -= 10;
                    if (issuedContainer.Height == issuedContainer.MinimumSize.Height)
                    {
                        issuedCollapse = true;
                        issuedTimer.Stop();
                    }
                }
            }
        }

        private void vudContainer_Resize(object sender, EventArgs e)
        {
            Panel resizedPanel = sender as Panel; // Cast sender to a Panel type
            if (resizedPanel.Height > maxHeight)
            {
                resizedPanel.Height = maxHeight;
            }
        }

       //------------------------------------------------------------------------------------------------------------------------------------

        //issued books search by member name ------------------------------------------------------------------------------------------------------------------


        public void fill_books_info_member_name()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from members_info";
            cmd1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(dt);
            dataGridView7.DataSource = dt;
            dataGridView7.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            con.Close();
        }

        private void dataGridView7_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            string i;
            i = dataGridView7.SelectedCells[0].Value.ToString();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from issue_books where member_enrollment='" + i.ToString() + "' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView6.DataSource = dt;
            dataGridView6.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            con.Close();
        }

        private void textBox14_KeyUp_1(object sender, KeyEventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from members_info where member_fullname like ('%" + textBox14.Text + "%')";
            cmd1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(dt);
            dataGridView7.DataSource = dt;
            dataGridView7.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            con.Close();
        }
        

        private void button13_Click(object sender, EventArgs e)
        {
            fill_books_info_member_name();
            panel21.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel20.Visible = false;
            panel22.Visible = false;
            panel24.Visible = false;
            panel27.Visible = false;
            panel29.Visible = false;
            panel26.Visible = true;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            fill_books_info_book_name();
            panel21.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel20.Visible = false;
            panel22.Visible = false;
            panel26.Visible = false;
            panel27.Visible = false;
            panel29.Visible = false;
            panel31.Visible = false;
            panel24.Visible = true;
        }

        //vud members  ------------------------------------------------------------------------------------------------------------------


        private void dataGridView8_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;
        }


        public void fill_grid_members()
        {
            dataGridView8.Columns.Clear();
            dataGridView8.Refresh();

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
               
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from members_info";
                cmd.ExecuteNonQuery();
                

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                dataGridView8.DataSource = null;
                dataGridView8.Rows.Clear();

                foreach (DataColumn column in dt.Columns)
                {
                    dataGridView8.Columns.Add(column.ColumnName, column.ColumnName);
                }

                DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                imageCol.HeaderText = "Member Image";
                imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imageCol.Width = 100;
                dataGridView8.Columns.Add(imageCol);

                foreach (DataRow row in dt.Rows)
                {
                    string imagePath = @"..\..\" + row["member_image"].ToString();

                    if (File.Exists(imagePath))
                    {
                        Image img = Image.FromFile(imagePath);
                        object[] rowData = row.ItemArray;
                        List<object> modifiedRowData = new List<object>(rowData);
                        modifiedRowData.Add(img);
                        dataGridView8.Rows.Add(modifiedRowData.ToArray());
                        dataGridView8.Rows[dataGridView8.Rows.Count - 1].Height = 100;
                    }
                    else
                    {
                        dataGridView8.Rows.Add(row.ItemArray);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void textBox16_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                label41.Visible = false;
                int count = 0;

                dataGridView8.Columns.Clear();
                dataGridView8.Refresh();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from members_info where member_fullname like('%" + textBox16.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                count = Convert.ToInt32(dt.Rows.Count.ToString());

                if (count > 0)
                {

                    dataGridView8.DataSource = null;
                    dataGridView8.Rows.Clear();

                    foreach (DataColumn column in dt.Columns)
                    {
                        dataGridView8.Columns.Add(column.ColumnName, column.ColumnName);
                    }

                    DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                    imageCol.HeaderText = "Member Image";
                    imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    imageCol.Width = 100;
                    dataGridView8.Columns.Add(imageCol);

                    foreach (DataRow row in dt.Rows)
                    {
                        string imagePath = @"..\..\" + row["member_image"].ToString();

                        if (File.Exists(imagePath))
                        {
                            Image img = Image.FromFile(imagePath);
                            object[] rowData = row.ItemArray;
                            List<object> modifiedRowData = new List<object>(rowData);
                            modifiedRowData.Add(img);
                            dataGridView8.Rows.Add(modifiedRowData.ToArray());
                            dataGridView8.Rows[dataGridView8.Rows.Count - 1].Height = 100;
                        }
                        else
                        {
                            dataGridView8.Rows.Add(row.ItemArray);
                        }
                    }
                }
                else
                {
                    label41.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView8.SelectedCells[0].Value.ToString());

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from members_info where member_enrollment_no =" + i + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                student_name.Text = dr["member_fullname"].ToString();
                student_enroll.Text = dr["member_enrollment_no"].ToString();
                student_contact.Text = dr["member_contact"].ToString();
                student_email.Text = dr["member_email"].ToString();
                membership.Text = dr["member_type"].ToString();
                username.Text = dr["member_username"].ToString();
                password.Text = dr["member_password"].ToString();
            }
            con.Close();
        }


        private void SelectImage()
        {
            // Check if any row is selected in the DataGridView
           // if (student_name.Text == "" || Convert.ToInt32(student_enroll.Text) == 0 || Convert.ToInt32(student_contact.Text) == 0 || student_email.Text == "" || membership.Text == "" || username.Text == "" || password.Text == "")
           // {
           //     MessageBox.Show("Please select a row first.");
            //    return;
           // }

            wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            openFileDialog3.Filter = "Image Files|*.jpeg;*.png;*.jpg;*.gif|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            result = openFileDialog3.ShowDialog();

            if (result == DialogResult.OK)
            {
                selectedImagePath = openFileDialog3.FileName;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
           // if (isFirstButtonClick)
           // {
                SelectImage();
             //   isFirstButtonClick = false;
               // button26.Enabled = false;
              //  label51.Visible = true;
                // Disable the button after the first click
           // }           
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                int i = Convert.ToInt32(dataGridView8.SelectedCells[0].Value.ToString());

                pwd1 = class1.GetRandomPassword(20);

                string img_path = "";
                string fileName = pwd1 + ".jpg";
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

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    cmd.CommandText = "update members_info set member_username='" + username.Text + "' ,member_password='" + password.Text + "' ,member_type='" + membership.Text + "' ,member_fullname='" + student_name.Text + "' ,member_image='" + img_path.ToString() + "'  ,member_contact='" + student_contact.Text + "' ,member_email='" + student_email.Text + "' where member_enrollment_no =" + i + "";
                }
                else
                {
                    cmd.CommandText = "update members_info set member_username='" + username.Text + "' ,member_password='" + password.Text + "' ,member_type='" + membership.Text + "' ,member_fullname='" + student_name.Text + "'   ,member_contact='" + student_contact.Text + "' ,member_email='" + student_email.Text + "' where member_enrollment_no =" + i + "";
                }

                textBox16.Text = "";
                student_name.Text = "";
                student_enroll.Text = "";
                student_contact.Text = "";
                student_email.Text = "";
                membership.Text = "";
                username.Text = "";
                password.Text = "";
                img_path = "";

                cmd.ExecuteNonQuery();
                fill_grid_members();
                con.Close();

                MessageBox.Show("Record Updated Successfully");

                img_path = "";
                destinationPath = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            selectedImagePath = "";
            wanted_path = "";
            openFileDialog3.Reset();
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            fill_grid_members();
            panel21.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel20.Visible = false;
            panel22.Visible = false;
            panel26.Visible = false;
            panel24.Visible = false;
            panel29.Visible = false;
            panel31.Visible = false;
            panel27.Visible = true;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            int y;
            y = Convert.ToInt32(dataGridView8.SelectedCells[0].Value.ToString());

            try
            {               
                {

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "delete from members_info where member_enrollment_no=@id";

                    cmd.Parameters.AddWithValue("@id", y);
                    
                    cmd.ExecuteNonQuery();
                    

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    fill_grid_members();
                    MessageBox.Show("Member deleted Successfully");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }


       //vud books-------------------------------------------------------------------------------------------------------------------------


        private void textBox18_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where book_name like('%" + textBox18.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView9.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox17_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where book_author_name like('%" + textBox17.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView9.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where book_author_name like('%" + textBox17.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());

                dataGridView9.DataSource = dt;
                con.Close();

                if (i == 0)
                {
                    label54.Visible = true;
                    dataGridView9.Visible = false;
                }
                else
                {
                    label54.Visible = false;
                    dataGridView9.Visible = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            int x = 0;
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where book_name like('%" + textBox18.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                x = Convert.ToInt32(dt.Rows.Count.ToString());

                dataGridView9.DataSource = dt;
                con.Close();

                if (x == 0)
                {
                    label54.Visible = true;
                    dataGridView9.Visible = false;
                }
                else
                {
                    label54.Visible = false;
                    dataGridView9.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void dataGridView9_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel30.Visible = true;
            int i;
            i = Convert.ToInt32(dataGridView9.SelectedCells[0].Value.ToString());
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where id=" + i + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    bookname.Text = dr["book_name"].ToString();
                    authorname.Text = dr["book_author_name"].ToString();
                    publicationname.Text = dr["book_publication_name"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dr["book_purchase_date"].ToString());
                    bookprice.Text = (dr["book_price"].ToString());
                    bookqty.Text = (dr["book_quantity"].ToString());

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            int z;
            z = Convert.ToInt32(dataGridView9.SelectedCells[0].Value.ToString());

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=yr222;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update books_info set book_name=@bookName, book_author_name=@authorName, book_publication_name=@publicationName, book_purchase_date=@purchaseDate, book_price=@bookPrice, book_quantity=@bookQuantity where id=@id";

                    cmd.Parameters.AddWithValue("@bookName", bookname.Text);
                    cmd.Parameters.AddWithValue("@authorName", authorname.Text);
                    cmd.Parameters.AddWithValue("@publicationName", publicationname.Text);
                    cmd.Parameters.AddWithValue("@purchaseDate", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@bookPrice", bookprice.Text);
                    cmd.Parameters.AddWithValue("@bookQuantity", bookqty.Text);
                    cmd.Parameters.AddWithValue("@id", z);

                    cmd.ExecuteNonQuery();
                    disp_books();
                    MessageBox.Show("Details Updated Successfully");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public void disp_books()
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView9.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            int y;
            y = Convert.ToInt32(dataGridView9.SelectedCells[0].Value.ToString());

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=yr222;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "delete from books_info where id=@id";

                    cmd.Parameters.AddWithValue("@id", y);

                    cmd.ExecuteNonQuery();
                    disp_books();
                    MessageBox.Show("Book deleted Successfully");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            disp_books();
            panel21.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel20.Visible = false;
            panel22.Visible = false;
            panel26.Visible = false;
            panel24.Visible = false;
            panel27.Visible = false;
            panel31.Visible = false;
            panel29.Visible = true;
        }

        private void admin_control_form_Click(object sender, EventArgs e)
        {
            if (sidebar.Width == sidebar.MaximumSize.Width)
            {
                sidebarExpand = true;
                sidebarTimer.Start();
            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            // Handle the click event for all panels here
            Panel clickedPanel = sender as Panel;
            if (clickedPanel != null)
            {
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Start();
                }              
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel22.Visible = false;
            panel27.Visible = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            panel21.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel20.Visible = false;
            panel22.Visible = false;
            panel26.Visible = false;
            panel24.Visible = false;
            panel27.Visible = false;
            panel29.Visible = false;
            panel31.Visible = true;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            this.Hide();
            login vb = new login();
            vb.ShowDialog();// Open the login form
            Application.Exit();
        }






        }
   }

